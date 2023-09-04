using BeerManagement.Services.Interfaces;
using BeerManagement.Models.DataModels;
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
        public IActionResult GetBeerDetailsById(int id)
        {
            if (id <= 0)
            {
                return SendReponse.BadRequestObjectResult("BeerId");
            }
            var result = _beerService.GetBeerDetailsById(id);

            return SendReponse.ReturnResponse(result);
        }
        [HttpGet]
        [Route("{gtAlcoholByVolume}/{ltAlcoholByVolume}")]
        public IActionResult GetAllBeersByAlchoholPercentage(decimal gtAlcoholByVolume = 0, decimal ltAlcoholByVolume = 0)
        {
            if (gtAlcoholByVolume < 0 || ltAlcoholByVolume < 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _beerService.GetAllBeersByAlchoholPercentage(gtAlcoholByVolume, ltAlcoholByVolume);

            return SendReponse.ReturnResponse(result);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateBeerDetails(int id, [FromBody] BeerModel beerInfo)
        {
            if (beerInfo == null)
            {
                return SendReponse.BadRequest();
            }

            if (id != beerInfo.BeerId || string.IsNullOrEmpty(beerInfo.BeerName) || id <= 0)
            {
                return SendReponse.BadRequest();
            }

            var result = _beerService.UpdateBeerDetails(beerInfo, out string statusMessage);

            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }
        [HttpPost]
        public IActionResult SaveNewBeerDetails([FromBody] BeerModel beerInfo)
        {
            if (beerInfo == null)
            {
                return SendReponse.BadRequest();
            }
            if (string.IsNullOrEmpty(beerInfo.BeerName) || beerInfo.PercentageAlcoholByVolume < 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _beerService.SaveNewBeerDetails(beerInfo, out string statusMessage);

            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }        
    }
}
