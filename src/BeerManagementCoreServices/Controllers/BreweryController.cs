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
        [Route("{id:int:min(1)}")]
        public IActionResult BreweryDetailsById(int id)
        {
            var result = _breweryService.BreweryDetailsById(id);
            if (result != null)
            {
                return SendReponse.ApiResponse(result);
            }
            return SendReponse.NoContentFound();
        }

        [HttpGet]
        public IActionResult AllBreweries()
        {
            var result = _breweryService.AllBreweries();
            if (result != null)
            {
                return SendReponse.ApiResponse(result);
            }
            return SendReponse.NoContentFound();
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public IActionResult BreweryDetailsUpdate(int id, [FromBody] BreweryModel breweryInfo)
        {
            if (id != breweryInfo.BreweryId)
            {
                return SendReponse.BadRequest();
            }
            if (ValidateInput(breweryInfo))
            {
                var result = _breweryService.BreweryDetailsUpdate(breweryInfo, out string statusMessage);
                if (result)
                {
                    return SendReponse.ApiResponse(result, statusMessage);
                }
                return SendReponse.BadRequestObjectResult(result, statusMessage);
            }
            return SendReponse.BadRequest();
        }

        [HttpPost]
        public IActionResult NewBrewery([FromBody] BreweryModel breweryInfo)
        {
            if (ValidateInput(breweryInfo))
            {
                var result = _breweryService.NewBrewery(breweryInfo, out string statusMessage);
                if (result)
                {
                    return SendReponse.ApiResponse(result, statusMessage);
                }
                return SendReponse.BadRequestObjectResult(result, statusMessage);
            }
            return SendReponse.BadRequest();
        }

        private bool ValidateInput(BreweryModel breweryInfo)
        {
            if (breweryInfo == null)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(breweryInfo.BreweryName))
            {
                return false;
            }
            return true;
        }
    }
}