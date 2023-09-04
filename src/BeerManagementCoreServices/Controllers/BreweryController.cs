using BeerManagement.Services.Interfaces;
using BeerManagement.Models.DataModels;
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
        public IActionResult GetBreweryDetailsById(int id)
        {
            if (id <= 0)
            {
                return SendReponse.BadRequestObjectResult("BreweryId");
            }
            var result = _breweryService.GetBreweryDetailsById(id);

            return SendReponse.ReturnResponse(result);
        }
        [HttpGet]
        public IActionResult GetAllBreweries()
        {
            var result = _breweryService.GetAllBreweries();

            return SendReponse.ReturnResponse(result);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateBreweryDetails(int id, [FromBody] BreweryModel breweryInfo)
        {
            if(breweryInfo == null)
            {
                return SendReponse.BadRequest();
            }  
            
            if(id != breweryInfo.BreweryId || string.IsNullOrEmpty(breweryInfo.BreweryName) || id<=0 )
            {
                return SendReponse.BadRequest();
            }

            var result = _breweryService.UpdateBreweryDetails(breweryInfo, out string statusMessage);

            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);            
        }
        [HttpPost]
        public IActionResult SaveNewBreweryDetails([FromBody] BreweryModel breweryInfo)
        {
            if (breweryInfo == null)
            {
                return SendReponse.BadRequest();
            }
            if (string.IsNullOrEmpty(breweryInfo.BreweryName))
            {
                return SendReponse.BadRequest();
            }

            var result = _breweryService.SaveNewBreweryDetails(breweryInfo, out string statusMessage);
            
            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }
    }
}
