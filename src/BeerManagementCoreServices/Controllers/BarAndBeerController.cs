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
            if (barAndBeer == null)
            {
                return SendReponse.BadRequest();
            }
            if (barAndBeer.BarId <= 0 || barAndBeer.BeerId <= 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _barAndBeerService.BarAndBeerLink(barAndBeer, out string statusMessage);
            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }

        [HttpGet]
        [Route("{barId:int}/beer")]
        public IActionResult BeersAssociatedWithBar(int barId)
        {
            if (barId <= 0)
            {
                return SendReponse.BadRequestObjectResult("BarId");
            }
            var result = _barAndBeerService.BeersAssociatedWithBar(barId);
            return SendReponse.ReturnResponse(result);
        }

        [HttpGet]
        [Route("beer")]
        public IActionResult BarsWithAssociatedBeers()
        {
            var result = _barAndBeerService.BarsWithAssociatedBeers();
            return SendReponse.ReturnResponse(result);
        }
    }
}