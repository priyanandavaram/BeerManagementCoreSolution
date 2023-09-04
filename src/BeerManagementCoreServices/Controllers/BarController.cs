using BeerManagement.Services.Interfaces;
using BeerManagement.Models.DataModels;
using BeerManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace BeerManagement.Web
{
    [ApiController]
    [Route("api/bar")]
    public class BarController
    {          
        private readonly IBarService _barService;
        public BarController(IBarService barService)
        {
            _barService = barService;
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetBarDetailsById(int id)
        {
            if (id <= 0)
            {
                return SendReponse.BadRequestObjectResult("BarId");
            }
            var result = _barService.GetBarDetailsById(id);

            return SendReponse.ReturnResponse(result);
        }
        [HttpGet]
        public IActionResult GetAllBars()
        {
            var result = _barService.GetAllBars();

            return SendReponse.ReturnResponse(result);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateBarDetails(int id, [FromBody] BarModel barInfo)
        {
            if (barInfo == null)
            {
                return SendReponse.BadRequest();
            }

            if (id != barInfo.BarId || string.IsNullOrEmpty(barInfo.BarName) || id <= 0)
            {
                return SendReponse.BadRequest();
            }

            var result = _barService.UpdateBarDetails(barInfo, out string statusMessage);

            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }
        [HttpPost]
        public IActionResult SaveNewBarDetails([FromBody] BarModel barInfo)
        {
            if (barInfo == null)
            {
                return SendReponse.BadRequest();
            }
            if (string.IsNullOrEmpty(barInfo.BarAddress) || string.IsNullOrEmpty(barInfo.BarName))
            {
                return SendReponse.BadRequest();
            }
            var result = _barService.SaveNewBarDetails(barInfo, out string statusMessage);

            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }
    }
}
