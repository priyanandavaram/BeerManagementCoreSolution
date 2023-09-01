using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BeerManagementCoreServices.Controllers
{
    [ApiController]
    [Route("api/beer")]
    public class BeerController
    {
        private readonly IBeerRepository _beerRepository;

        public BeerController(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }
        [HttpGet]
        [Route("{id:int}")]
        public Beers GetBeerDetailsById(int id)
        {
            return _beerRepository.GetBeerDetailsById(id);
        }
        [HttpGet]
        [Route("{gtAlcoholByVolume}/{ltAlcoholByVolume}")]
        public List<Beers> GetAllBeersByAlchoholPercentage(decimal gtAlcoholByVolume = 0, decimal ltAlcoholByVolume = 0)
        {
            return _beerRepository.GetAllBeersByAlchoholPercentage(gtAlcoholByVolume, ltAlcoholByVolume);
        }
        [HttpPut]
        [Route("{id}")]
        public string UpdateBeerDetails(int id, [FromBody] Beers updateBeerDetails)
        {
            updateBeerDetails.BeerId = id;

            var message = _beerRepository.UpdateBeerDetails(updateBeerDetails);

            return message;
        }
        [HttpPost]
        [Route("")]
        public string SaveNewBeerDetails([FromBody] Beers createNewBeer)
        {
            var message = _beerRepository.SaveNewBeerDetails(createNewBeer);

            return message;
        }
    }
}
