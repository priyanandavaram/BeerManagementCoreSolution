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
        [Route("{id:int:min(1)}")]
        public IActionResult BeerDetailsById(int id)
        {
            var result = _beerService.BeerDetailsById(id);
            if (result != null)
            {
                return SendReponse.ApiResponse(result);
            }
            return SendReponse.NoContentFound();
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
            if (result != null)
            {
                return SendReponse.ApiResponse(result);
            }
            return SendReponse.NoContentFound();
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public IActionResult BeerDetailsUpdate(int id, [FromBody] BeerModel beerInfo)
        {
            if (id != beerInfo.BeerId)
            {
                return SendReponse.BadRequest();
            }
            if (ValidateInput(beerInfo))
            {
                var result = _beerService.BeerDetailsUpdate(beerInfo, out string statusMessage);
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

        [HttpPost]
        public IActionResult NewBeer([FromBody] BeerModel beerInfo)
        {
            if (ValidateInput(beerInfo))
            {
                var result = _beerService.NewBeer(beerInfo, out string statusMessage);
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

        private bool ValidateInput(BeerModel beerInfo)
        {
            if (beerInfo == null)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(beerInfo.BeerName))
            {
                return false;
            }
            else if (beerInfo.PercentageAlcoholByVolume < 0)
            {
                return false;
            }
            return true;
        }
    }
}