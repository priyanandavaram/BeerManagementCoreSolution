using BeerManagement.Models;
using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;
namespace BeerManagement.Web
{
    [ApiController]
    [Route("api/beer")]
    public class BeerController
    {
        private readonly IBeerService _beerService;
        public BeerController(IBeerService beerService)
        {
            _beerService = beerService;
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult BeerDetailsById(int id)
        {
            if (id <= 0)
            {
                return SendReponse.BadRequestObjectResult("BeerId");
            }
            var result = _beerService.BeerDetailsById(id);
            return SendReponse.ReturnResponse(result);
        }

        [HttpGet]
        [Route("{gtAlcoholByVolume}&{ltAlcoholByVolume}")]
        public IActionResult AllBeersByAlchoholPercentage(decimal gtAlcoholByVolume = 0, decimal ltAlcoholByVolume = 0)
        {
            if (gtAlcoholByVolume < 0 || ltAlcoholByVolume < 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _beerService.AllBeersByAlchoholPercentage(gtAlcoholByVolume, ltAlcoholByVolume);
            return SendReponse.ReturnResponse(result);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult BeerDetailsUpdate(int id, [FromBody] BeerModel beerInfo)
        {
            if (beerInfo == null)
            {
                return SendReponse.BadRequest();
            }
            if (id != beerInfo.BeerId || string.IsNullOrEmpty(beerInfo.BeerName) || id <= 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _beerService.BeerDetailsUpdate(beerInfo, out string statusMessage);
            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }

        [HttpPost]
        public IActionResult NewBeer([FromBody] BeerModel beerInfo)
        {
            if (beerInfo == null)
            {
                return SendReponse.BadRequest();
            }
            if (string.IsNullOrEmpty(beerInfo.BeerName) || beerInfo.PercentageAlcoholByVolume < 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _beerService.NewBeer(beerInfo, out string statusMessage);
            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }
    }
}