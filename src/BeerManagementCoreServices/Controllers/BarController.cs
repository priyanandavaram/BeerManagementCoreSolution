using BeerManagement.Models;
using BeerManagement.Services.Interfaces;
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
        public IActionResult BarDetailsById(int id)
        {
            if (id <= 0)
            {
                return SendReponse.BadRequestObjectResult("BarId");
            }
            var result = _barService.BarDetailsById(id);
            return SendReponse.ReturnResponse(result);
        }

        [HttpGet]
        public IActionResult AllBars()
        {
            var result = _barService.AllBars();
            return SendReponse.ReturnResponse(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult BarDetailsUpdate(int id, [FromBody] BarModel barInfo)
        {
            if (barInfo == null)
            {
                return SendReponse.BadRequest();
            }
            if (id != barInfo.BarId || string.IsNullOrEmpty(barInfo.BarName) || id <= 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _barService.BarDetailsUpdate(barInfo, out string statusMessage);
            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }

        [HttpPost]
        public IActionResult NewBar([FromBody] BarModel barInfo)
        {
            if (barInfo == null)
            {
                return SendReponse.BadRequest();
            }
            if (string.IsNullOrEmpty(barInfo.BarAddress) || string.IsNullOrEmpty(barInfo.BarName))
            {
                return SendReponse.BadRequest();
            }
            var result = _barService.NewBar(barInfo, out string statusMessage);
            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);
        }
    }
}