using Domain;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Domain
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private ApplicationDbContext applicationDbContext;
        private UserManager<ApplicationUser> userManager;
        public ApplicationUserController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult> GetTokensPerUser()
        {
            ApplicationUser applicationUser = await userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(applicationUser);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAllUsers()
        {
            return Ok(applicationDbContext.Users
                .Include(x => x.UsersFollowing)
                .Include(x => x.UsersFollowedBy)
                .Include(x => x.EnergyRessourcesConsumed)
                .Include(x => x.EnergyRessourcesProduced)
                .Select(user => new ApplicationUserDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Address = user.Address,
                    Tokens = user.Tokens,
                    EnergySupplier = user.EnergySupplier,
                    EnergyRessourcesConsumed = user.EnergyRessourcesConsumed.Select(x => 
                        new EnergyRessourceDTO 
                        {
                            Duration = x.Duration.Days,
                            ProductionDayRainnykWh = x.ProductionDayRainnykWh,
                            ProductionDaySunnykWh = x.ProductionDaySunnykWh,
                            ConsumerId = x.ConsumerId,
                            Source = x.Source,
                            Id = x.Id,
                            PricekWh = x.PricePerkWh,
                            ProducerId = x.ProducerId,
                            ProductionNightkWh = x.ProductionNightkWh
                        }).ToList(),
                    EnergyRessourcesProduced = user.EnergyRessourcesProduced.Select(x =>
                        new EnergyRessourceDTO
                        {
                            Duration = x.Duration.Days,
                            ProductionDayRainnykWh = x.ProductionDayRainnykWh,
                            ProductionDaySunnykWh = x.ProductionDaySunnykWh,
                            ConsumerId = x.ConsumerId,
                            Source = x.Source,
                            Id = x.Id,
                            PricekWh = x.PricePerkWh,
                            ProducerId = x.ProducerId,
                            ProductionNightkWh = x.ProductionNightkWh
                        }).ToList(),
                    UsersFollowedBy = user.UsersFollowedBy.Select(x => new ApplicationUserFollowerPairDTO
                    {
                        FollowedId = x.FollowedId,
                        FollowerId = x.FollowerId
                    }).ToList(),
                    UsersFollowing = user.UsersFollowing.Select(x => new ApplicationUserFollowerPairDTO
                    {
                        FollowedId = x.FollowedId,
                        FollowerId = x.FollowerId
                    }).ToList(),
                }));
        }
    }
}
