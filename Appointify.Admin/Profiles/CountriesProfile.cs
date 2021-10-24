using AutoMapper;

using Appointify.Data.Entities;
using Appointify.Admin.ViewModels.Countries;

namespace Appointify.Admin.Profiles
{
    public class CountriesProfile : Profile
    {
        public CountriesProfile()
        {
            CreateMap<EditViewModel, Country>().ReverseMap();
            CreateMap<AddViewModel, Country>();
        }
    }
}
