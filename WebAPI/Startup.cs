using IdentityServer4.Models;
using Domain;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Hubs;
using Infrastructure.Identity;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment webHostEnvironment;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllers();
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .AllowAnyHeader()
                          .Build();
                });
            });
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            #region EF Core Registration
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["AzureSQLConnection"]);
            });
            #endregion
            #region ASP.NET Core Identity Registration
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                options.DefaultChallengeScheme = IdentityServerJwtConstants.IdentityServerJwtBearerScheme;
                options.DefaultAuthenticateScheme = "ApplicationDefinedAuthentication";
            })
                .AddIdentityServerJwt()
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["GoogleClientId"];
                    options.ClientSecret = Configuration["GoogleClientSecret"];
                    options.Scope.Add("profile");
                    options.Events.OnCreatingTicket = (context) =>
                    {
                        var picture = context.User.GetProperty("picture").GetString();
                        context.Identity.AddClaim(new Claim("picture", picture));
                        return Task.CompletedTask;
                    };
                })
                .AddGitHub(options =>
                {
                    options.ClientId = Configuration["GitHubClientId"];
                    options.ClientSecret = Configuration["GitHubClientSecret"];
                    options.Events.OnCreatingTicket = context =>
                    {
                        string picUri = context.User.GetProperty("avatar_url").GetString();
                        context.Identity.AddClaim(new Claim("picture", picUri));
                        return Task.CompletedTask;
                    };
                })
                .AddPolicyScheme("ApplicationDefinedAuthentication", null, options =>
                {
                    options.ForwardDefaultSelector = (context) =>
                    {
                        if (context.Request.Path.StartsWithSegments(new PathString("/api"), StringComparison.OrdinalIgnoreCase))
                            return IdentityServerJwtConstants.IdentityServerJwtScheme;
                        else
                            return IdentityConstants.ApplicationScheme;
                    };
                })
                .AddIdentityCookies(options =>
                {
                });
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Login";
                config.LogoutPath = "/Logout";
            });

            var identityService = services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.RequireUniqueEmail = false;
                options.Tokens.AuthenticatorIssuer = "JustRoll";
                options.Stores.MaxLengthForKeys = 128;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            identityService.AddSignInManager();
            #endregion
            #region IdentityServer Registration
            if (webHostEnvironment.IsDevelopment())
            {
                services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    options.Clients.Add(new Client
                    {
                        ClientId = "BlazorClient",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequirePkce = true,
                        RequireClientSecret = false,
                        AllowedScopes = new List<string>
                        {
                            "openid",
                            "profile",
                            "API"
                        },
                        RedirectUris = { "https://localhost:44313/authentication/login-callback" },
                        PostLogoutRedirectUris = { "https://localhost:44313" },
                        FrontChannelLogoutUri = "https://localhost:44313"
                    });
                    options.ApiResources = new ApiResourceCollection
                    {
                        new ApiResource
                        {
                            Name = "API",
                            Scopes = new List<string> {"API"}
                        }
                    };
                    var cert = options.SigningCredential = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("David Eggenberger Security key very long very secure")), SecurityAlgorithms.HmacSha256);
                })
                .AddProfileService<ProfileService>();
            }
            if (webHostEnvironment.IsProduction())
            {
                services.AddIdentityServer(options =>
                {

                })
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    options.Clients.Add(new Client
                    {
                        ClientId = "BlazorClient",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequirePkce = true,
                        RequireClientSecret = false,
                        AllowedScopes = new List<string>
                        {
                            "openid",
                            "profile",
                            "API"
                        },
                        RedirectUris =
                            {
                                "https://localhost:44313/authentication/login-callback",
                                "https://localhost:44313/authentication/login-callback"
                            },
                        PostLogoutRedirectUris =
                            {
                                "https://localhost:44313",
                                "https://localhost:44313",
                            },
                        FrontChannelLogoutUri = "https://localhost:44313"
                    });
                    options.ApiResources = new ApiResourceCollection
                    {
                        new ApiResource
                        {
                            Name = "API",
                            Scopes = new List<string> {"API"}
                        }
                    };
                    var cert = options.SigningCredential = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("David Eggenberger Security key very long very secure")), SecurityAlgorithms.HmacSha256);
                })
                .AddProfileService<ProfileService>();
            }
            #endregion
            services.Configure<JwtBearerOptions>(IdentityServerJwtConstants.IdentityServerJwtBearerScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = "API"
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/ExceptionHandler");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<PriceHub>("/pricehub");
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
