using BeerManagement.Services.Interfaces;
using BeerManagement.Models.DataModels;
using BeerManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BeerManagement.Web
{
    [ApiController]
    [Route("api/brewery")]
    public class BreweryAndBeerController
    {
        private readonly IBreweryAndBeerService _breweryAndBeerService;

        public BreweryAndBeerController(IBreweryAndBeerService breweryAndBeerService)
        {
            _breweryAndBeerService = breweryAndBeerService;  
        }

        [HttpPost]
        [Route("beer")]
        public IActionResult LinkBreweryAndBeer([FromBody] BreweryAndBeerModel breweryAndBeer)
        {
            if(breweryAndBeer == null)
            {
                return SendReponse.BadRequest();
            }
            if(breweryAndBeer.BreweryId <=0 || breweryAndBeer.BeerId <= 0)
            {
                return SendReponse.BadRequest();
            }
            var result = _breweryAndBeerService.LinkBreweryAndBeer(breweryAndBeer, out string statusMessage);

            return SendReponse.ReturnResponseByBooleanValue(result, statusMessage);            
        }
        [HttpGet]
        [Route("{breweryId:int}/beer")]
        public IActionResult GetAllBeersAssociatedWithBrewery(int breweryId)
        {
            if (breweryId <= 0)
            {
                return SendReponse.BadRequestObjectResult("BreweryId");
            }
            var result = _breweryAndBeerService.GetAllBeersAssociatedWithBrewery(breweryId);

            return SendReponse.ReturnResponse(result);            
        }
        [HttpGet]
        [Route("beer")]
        public IActionResult GetAllBreweriesWithAssociatedBeers()
        {
            try
            {
                var result = _breweryAndBeerService.GetAllBreweriesWithAssociatedBeers();

                return SendReponse.ReturnResponse(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
