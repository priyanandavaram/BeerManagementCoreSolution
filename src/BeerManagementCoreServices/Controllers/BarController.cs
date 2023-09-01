using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Database;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BeerManagementCoreServices.Controllers
{
    [ApiController]
    [Route("api/bar")]
    public class BarController
    {          
        private readonly IBarRepository _barRepository;
        public BarController(IBarRepository barRepository)
        {
            _barRepository = barRepository;
        }
        [HttpGet]
        [Route("{id:int}")]
        public Bars GetBarDetailsById(int id)
        {
            return _barRepository.GetBarDetailsById(id);
        }
        [HttpGet]
        [Route("")]
        public List<Bars> GetAllBars()
        {
            return _barRepository.GetAllBars();
        }
        [HttpPut]
        [Route("{id:int}")]
        public string UpdateBarDetails(int id, [FromBody] Bars updateBarDetails)
        {
            updateBarDetails.BarId = id;

            var message = _barRepository.UpdateBarDetails(updateBarDetails);

            return message;
        }
        [HttpPost]
        [Route("")]
        public string SaveNewBarDetails([FromBody] Bars createNewBar)
        {
            var message = _barRepository.SaveNewBarDetails(createNewBar);

            return message;
        }
    }
}
