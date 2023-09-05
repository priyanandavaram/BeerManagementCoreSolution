using BeerManagement.Models;
using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;
namespace BeerManagement.Web
{
    [ApiController]
    [Route("api/brewery")]
    public class BreweryAndBeerController
    {
        private readonly IBreweryAndBeerService _breweryAndBeerService;
        public BreweryAndBeerController(IBreweryAndBeerService breweryAndBeerService)
        {
            _breweryAndBeerService = breweryAndBeerService;
        }
        [HttpPost]
        [Route("beer")]
        public IActionResult BreweryAndBeerLink([FromBody] BreweryAndBeerModel breweryAndBeer)
        {
            if (breweryAndBeer == null)
            {
                return SendReponse.BadRequest();
            }
            if (breweryAndBeer.BreweryId <= 0 || breweryAndBeer.BeerId <= 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _breweryAndBeerService.BreweryAndBeerLink(breweryAndBeer, out string statusMessage);

            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }

        [HttpGet]
        [Route("{breweryId:int}/beer")]
        public IActionResult AllBeersAssociatedWithBrewery(int breweryId)
        {
            if (breweryId <= 0)
            {
                return SendReponse.BadRequestObjectResult("BreweryId");
            }
            var result = _breweryAndBeerService.AllBeersAssociatedWithBrewery(breweryId);

            return SendReponse.ReturnResponse(result);
        }

        [HttpGet]
        [Route("beer")]
        public IActionResult AllBreweriesWithAssociatedBeers()
        {
            var result = _breweryAndBeerService.AllBreweriesWithAssociatedBeers();

            return SendReponse.ReturnResponse(result);
        }
    }
}