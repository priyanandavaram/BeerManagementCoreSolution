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
            if (ValidateInput(breweryAndBeer))
            {
                var result = _breweryAndBeerService.BreweryAndBeerLink(breweryAndBeer, out string statusMessage);
                if (result)
                {
                    return SendReponse.ApiResponse(result, statusMessage);
                }
                return SendReponse.BadRequestObjectResult(result, statusMessage);
            }
            else
            {
                return SendReponse.BadRequest();
            }
        }

        [HttpGet]
        [Route("{breweryId:int:min(1)}/beer")]
        public IActionResult AllBeersAssociatedWithBrewery(int breweryId)
        {
            var result = _breweryAndBeerService.AllBeersAssociatedWithBrewery(breweryId);
            if (result != null)
            {
                return SendReponse.ApiResponse(result);
            }
            return SendReponse.NoContentFound();
        }

        [HttpGet]
        [Route("beer")]
        public IActionResult AllBreweriesWithAssociatedBeers()
        {
            var result = _breweryAndBeerService.AllBreweriesWithAssociatedBeers();
            if (result != null)
            {
                return SendReponse.ApiResponse(result);
            }
            return SendReponse.NoContentFound();
        }

        private bool ValidateInput(BreweryAndBeerModel breweryAndBeer)
        {
            if (breweryAndBeer == null)
            {
                return false;
            }
            if (breweryAndBeer.BreweryId < 1 || breweryAndBeer.BeerId < 1)
            {
                return false;
            }
            return true;
        }
    }
}