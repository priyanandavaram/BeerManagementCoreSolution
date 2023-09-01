using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BeerManagementCoreServices.Controllers
{
    [ApiController]
    [Route("api/bar")]
    public class LinkBarAndBeerController
    {
        private readonly ILinkBarAndBeerRepository _linkBarAndBeerRepository;
        public LinkBarAndBeerController(ILinkBarAndBeerRepository linkBarAndBeerRepository)
        {
            _linkBarAndBeerRepository = linkBarAndBeerRepository;
        }
        [HttpPost]
        [Route("beer")]
        public string LinkBarAndBeer([FromBody] LinkBarWithBeer linkWithBeer)
        {
            var message = _linkBarAndBeerRepository.LinkBarAndBeer(linkWithBeer);

            return message;
        }
        [HttpGet]
        [Route("{barId:int}/beer")]
        public List<BarWithAssociatedBeersModel> GetAllBeersAssociatedWithBar(int barId)
        {
            return _linkBarAndBeerRepository.GetAllBeersAssociatedWithBar(barId);
        }
        [HttpGet]
        [Route("beer")]
        public List<BarWithAssociatedBeersModel> GetAllBarsWithAssociatedBeers()
        {
            return _linkBarAndBeerRepository.GetAllBarsWithAssociatedBeers();
        }

    }
}
