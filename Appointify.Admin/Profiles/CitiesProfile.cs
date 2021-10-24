using AutoMapper;

using Appointify.Data.Entities;
using Appointify.Admin.ViewModels.Cities;

namespace Appointify.Admin.Profiles
{
    public class CitiesProfile : Profile
    {
        public CitiesProfile()
        {
            CreateMap<EditViewModel, City>().ReverseMap();
            CreateMap<AddViewModel, City>();
        }
    }
}
