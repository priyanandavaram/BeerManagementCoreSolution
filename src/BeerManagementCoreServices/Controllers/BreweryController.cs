using BeerManagement.Models;
using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;
namespace BeerManagement.Web
{
    [ApiController]
    [Route("api/brewery")]
    public class BreweryController
    {
        private readonly IBreweryService _breweryService;
        public BreweryController(IBreweryService breweryService)
        {
            _breweryService = breweryService;
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult BreweryDetailsById(int id)
        {
            if (id <= 0)
            {
                return SendReponse.BadRequestObjectResult("BreweryId");
            }
            var result = _breweryService.BreweryDetailsById(id);
            return SendReponse.ReturnResponse(result);
        }

        [HttpGet]
        public IActionResult AllBreweries()
        {
            var result = _breweryService.AllBreweries();
            return SendReponse.ReturnResponse(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult BreweryDetailsUpdate(int id, [FromBody] BreweryModel breweryInfo)
        {
            if (breweryInfo == null)
            {
                return SendReponse.BadRequest();
            }
            if (id != breweryInfo.BreweryId || string.IsNullOrEmpty(breweryInfo.BreweryName) || id <= 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _breweryService.BreweryDetailsUpdate(breweryInfo, out string statusMessage);
            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }

        [HttpPost]
        public IActionResult NewBrewery([FromBody] BreweryModel breweryInfo)
        {
            if (breweryInfo == null)
            {
                return SendReponse.BadRequest();
            }
            if (string.IsNullOrEmpty(breweryInfo.BreweryName))
            {
                return SendReponse.BadRequest();
            }
            var result = _breweryService.NewBrewery(breweryInfo, out string statusMessage);
            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }
    }
}