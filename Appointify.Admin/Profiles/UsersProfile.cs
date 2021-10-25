using AutoMapper;

using Appointify.Data.Entities;
using Appointify.Admin.ViewModels.Users;

namespace Appointify.Admin.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<AddViewModel, User>();
        }
    }
}
