using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Models;
using BeerManagement.Models.DataModels;
using AutoMapper;

namespace BeerManagement.Services.Automapper
{
    public class AutoMapperClass : Profile
    {
        public AutoMapperClass()
        {
            CreateMap<BreweryModel, Brewery>().ReverseMap();
            CreateMap<Brewery, BreweryModel>().ReverseMap();

            CreateMap<Beers, BeerModel>().ReverseMap();
            CreateMap<BeerModel, Beers>().ReverseMap();

            CreateMap<Bars, BarModel>().ReverseMap();
            CreateMap<BarModel, Bars>().ReverseMap();

            CreateMap<BreweryAndBeerModel, LinkBreweryWithBeer>().ReverseMap();
            CreateMap<LinkBreweryWithBeer, BreweryAndBeerModel>().ReverseMap();


            CreateMap<BarAndBeerModel, LinkBarWithBeer>().ReverseMap();
            CreateMap<LinkBarWithBeer, BarAndBeerModel>().ReverseMap();

            CreateMap<LinkBreweryWithBeer, BreweryWithAssociatedBeersModel>().ReverseMap();

        }
    }
}
