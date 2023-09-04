using BeerManagement.Services.Interfaces;
using BeerManagement.Models.DataModels;
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
        public IActionResult LinkBarAndBeer([FromBody] BarAndBeerModel barAndBeer)
        {
            if (barAndBeer == null)
            {
                return SendReponse.BadRequest();
            }
            if (barAndBeer.BarId <= 0 || barAndBeer.BeerId <= 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _barAndBeerService.LinkBarAndBeer(barAndBeer, out string statusMessage);

            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);            
        }
        [HttpGet]
        [Route("{barId:int}/beer")]
        public IActionResult GetAllBeersAssociatedWithBar(int barId)
        {
            if (barId <= 0)
            {
                return SendReponse.BadRequestObjectResult("BarId");
            }
            var result = _barAndBeerService.GetAllBeersAssociatedWithBar(barId);

            return SendReponse.ReturnResponse(result);
        }
        [HttpGet]
        [Route("beer")]
        public IActionResult GetAllBarsWithAssociatedBeers()
        {
            var result = _barAndBeerService.GetAllBarsWithAssociatedBeers();

            return SendReponse.ReturnResponse(result);
        }
    }
}
