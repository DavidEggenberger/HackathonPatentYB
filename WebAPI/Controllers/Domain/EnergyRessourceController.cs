using Domain;
using Infrastructure.Persistance;
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
    public class EnergyRessourceController : ControllerBase
    {
        private ApplicationDbContext applicationDbContext;
        private UserManager<ApplicationUser> userManager;
        public EnergyRessourceController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }
        [HttpGet("allOffers")]
        public async Task<ActionResult> GetAllEnergyRessoucesToBeBought()
        {
            return Ok(applicationDbContext.EnergyRessources
                .Include(x => x.Consumer)
                .Include(x => x.Producer)
                .Where(x => x.Consumer == null)
                .Select(x => new EnergyRessourceDTO
                {
                    Duration = x.Duration,
                    ProductionDayRainny = x.ProductionDayRainny,
                    ProductionDaySunny = x.ProductionDaySunny,
                    Id = x.Id,
                    Price = x.Price,
                    Source = x.Source,
                    ProductionNight = x.ProductionNight,
                    ProducerId = x.ProducerId
                }));
        }
        [HttpPost]
        public async Task<ActionResult> CreateNew(EnergyRessourceDTO energyRessourceDTO)
        {
            ApplicationUser applicationUser = await userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            EnergyRessource energyRessource = new EnergyRessource()
            {
                Duration = energyRessourceDTO.Duration,
                ProductionDayRainny = energyRessourceDTO.ProductionDayRainny,
                ProductionDaySunny = energyRessourceDTO.ProductionDaySunny,
                Price = energyRessourceDTO.Price,
                Source = energyRessourceDTO.Source,
                ProductionNight = energyRessourceDTO.ProductionNight,
                ProducerId = applicationUser.Id
            };
            applicationDbContext.EnergyRessources.Add(energyRessource);
            int i = await applicationDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("buyEnergyRessource/{energyRessourceId}")]
        public async Task<ActionResult> BuyEnergyRessource(Guid energyRessourceId)
        {
            ApplicationUser applicationUser = await userManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            EnergyRessource energyRessource = applicationDbContext.EnergyRessources
                                                        .Single(x => x.Id == energyRessourceId);
            
            if(applicationUser.Tokens < energyRessource.Price)
            {
                return BadRequest();
            }

            energyRessource.Consumer = applicationUser;
            await applicationDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
