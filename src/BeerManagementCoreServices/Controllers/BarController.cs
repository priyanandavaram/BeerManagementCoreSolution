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
        [Route("{id:int:min(1)}")]
        public IActionResult BarDetailsById(int id)
        {
            var result = _barService.BarDetailsById(id);
            if (result != null)
            {
                return SendReponse.ApiResponse(result);
            }
            return SendReponse.NoContentFound();
        }

        [HttpGet]
        public IActionResult AllBars()
        {
            var result = _barService.AllBars();
            if (result != null)
            {
                return SendReponse.ApiResponse(result);
            }
            return SendReponse.NoContentFound();
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        public IActionResult BarDetailsUpdate(int id, [FromBody] BarModel barInfo)
        {
            if (id != barInfo.BarId)
            {
                return SendReponse.BadRequest();
            }
            if (ValidateInput(barInfo))
            {
                var result = _barService.BarDetailsUpdate(barInfo, out string statusMessage);
                if (result)
                {
                    return SendReponse.ApiResponse(result, statusMessage);
                }
                return SendReponse.BadRequestObjectResult(result, statusMessage);
            }
            return SendReponse.BadRequest();
        }

        [HttpPost]
        public IActionResult NewBar([FromBody] BarModel barInfo)
        {
            if (ValidateInput(barInfo))
            {
                var result = _barService.NewBar(barInfo, out string statusMessage);
                if (result)
                {
                    return SendReponse.ApiResponse(result, statusMessage);
                }
                return SendReponse.BadRequestObjectResult(result, statusMessage);
            }
            return SendReponse.BadRequest();
        }

        private bool ValidateInput(BarModel barInfo)
        {
            if (barInfo == null)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(barInfo.BarName) || string.IsNullOrEmpty(barInfo.BarAddress))
            {
                return false;
            }
            return true;
        }
    }
}