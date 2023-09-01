using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BeerManagementCoreServices.Controllers
{
    [ApiController]
    [Route("api/brewery")]
    public class BreweryController
    {
        private readonly IBreweryRepository _breweryRepository;
        public BreweryController(IBreweryRepository breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }
        [HttpGet]
        [Route("{id:int}")]
        public Brewery GetBreweryDetailsById(int id)
        {
            return _breweryRepository.GetBreweryDetailsById(id);
        }
        [HttpGet]
        [Route("")]
        public List<Brewery> GetAllBreweries()
        {
            return _breweryRepository.GetAllBreweries();
        }
        [HttpPut]
        [Route("{id:int}")]
        public string UpdateBreweryDetails(int id, [FromBody] Brewery updateBreweryDetails)
        {
            updateBreweryDetails.BreweryId = id;

            var message = _breweryRepository.UpdateBreweryDetails(updateBreweryDetails);

            return message;
        }
        [HttpPost]
        [Route("")]
        public string SaveNewBreweryDetails([FromBody] Brewery createNewBrewery)
        {
            var message = _breweryRepository.SaveNewBreweryDetails(createNewBrewery);

            return message;
        }
    }
}
