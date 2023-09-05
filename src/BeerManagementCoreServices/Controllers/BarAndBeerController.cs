using BeerManagement.Models;
using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;
namespace BeerManagement.Web
{
    [ApiController]
    [Route("api/bar")]
    public class BarAndBeerController
    {
        private readonly IBarAndBeerService _barAndBeerService;
        public BarAndBeerController(IBarAndBeerService barAndBeerService)
        {
            _barAndBeerService = barAndBeerService;
        }
        [HttpPost]
        [Route("beer")]
        public IActionResult BarAndBeerLink([FromBody] BarAndBeerModel barAndBeer)
        {
            if (ValidateInput(barAndBeer))
            {
                var result = _barAndBeerService.BarAndBeerLink(barAndBeer, out string statusMessage);
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
        [Route("{barId:int:min(1)}/beer")]
        public IActionResult BeersAssociatedWithBar(int barId)
        {
            var result = _barAndBeerService.BeersAssociatedWithBar(barId);
            if (result != null)
            {
                return SendReponse.ApiResponse(result);
            }
            return SendReponse.NoContentFound();
        }

        [HttpGet]
        [Route("beer")]
        public IActionResult BarsWithAssociatedBeers()
        {
            var result = _barAndBeerService.BarsWithAssociatedBeers();
            if (result != null)
            {
                return SendReponse.ApiResponse(result);
            }
            return SendReponse.NoContentFound();
        }

        private bool ValidateInput(BarAndBeerModel barAndBeer)
        {
            if (barAndBeer == null)
            {
                return false;
            }
            else if (barAndBeer.BarId < 1)
            {
                return false;
            }
            else if (barAndBeer.BeerId < 1)
            {
                return false;
            }
            return true;
        }
    }
}