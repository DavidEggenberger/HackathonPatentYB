using Domain;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Domain
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private ApplicationDbContext applicationDbContext;
        public ApplicationUserController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
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
                            Duration = x.Duration,
                            ProductionDayRainny = x.ProductionDayRainny,
                            ProductionDaySunny = x.ProductionDaySunny,
                            ConsumerId = x.ConsumerId,
                            Source = x.Source,
                            Id = x.Id,
                            Price = x.Price,
                            ProducerId = x.ProducerId,
                            ProductionNight = x.ProductionNight
                        }).ToList(),
                    EnergyRessourcesProduced = user.EnergyRessourcesProduced.Select(x =>
                        new EnergyRessourceDTO
                        {
                            Duration = x.Duration,
                            ProductionDayRainny = x.ProductionDayRainny,
                            ProductionDaySunny = x.ProductionDaySunny,
                            ConsumerId = x.ConsumerId,
                            Source = x.Source,
                            Id = x.Id,
                            Price = x.Price,
                            ProducerId = x.ProducerId,
                            ProductionNight = x.ProductionNight
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
