using Domain;
using Infrastructure.Persistance;
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
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyRessourceController : ControllerBase
    {
        private ApplicationDbContext applicationDbContext;
        public EnergyRessourceController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
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
            EnergyRessource energyRessource = new EnergyRessource()
            {
                Duration = energyRessourceDTO.Duration,
                ProductionDayRainny = energyRessourceDTO.ProductionDayRainny,
                ProductionDaySunny = energyRessourceDTO.ProductionDaySunny,
                Price = energyRessourceDTO.Price,
                Source = energyRessourceDTO.Source,
                ProductionNight = energyRessourceDTO.ProductionNight,
                ProducerId = energyRessourceDTO.ProducerId
            };
            applicationDbContext.EnergyRessources.Add(energyRessource);
            await applicationDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
