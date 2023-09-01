using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BeerManagementCoreServices.Controllers
{
    [ApiController]
    [Route("api/brewery")]
    public class LinkBreweryAndBeerController
    {
        private readonly ILinkBreweryAndBeerRepository _linkBreweryAndBeerRepository;

        public LinkBreweryAndBeerController(ILinkBreweryAndBeerRepository linkBreweryAndBeerRepository)
        {
            _linkBreweryAndBeerRepository = linkBreweryAndBeerRepository;  
        }

        [HttpPost]
        [Route("beer")]
        public string LinkBreweryAndBeer([FromBody] LinkBreweryWithBeer linkWithBeer)
        {
            var message = _linkBreweryAndBeerRepository.LinkBreweryAndBeer(linkWithBeer);

            return message;
        }
        [HttpGet]
        [Route("{breweryId:int}/beer")]
        public List<BreweryWithAssociatedBeersModel> GetAllBeersAssociatedWithBrewery(int breweryId)
        {
            return _linkBreweryAndBeerRepository.GetAllBeersAssociatedWithBrewery(breweryId);
        }
        [HttpGet]
        [Route("beer")]
        public List<BreweryWithAssociatedBeersModel> GetAllBreweriesWithAssociatedBeers()
        {
            return _linkBreweryAndBeerRepository.GetAllBreweriesWithAssociatedBeers();
        }
    }
}
