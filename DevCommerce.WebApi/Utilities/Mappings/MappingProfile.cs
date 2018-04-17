using AutoMapper;
using DevCommerce.Entities.Concrete;
using DevCommerce.Web.Framework.Models;

namespace DevCommerce.WebApi.Utilities.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TokenViewModel, Token>();
            CreateMap<RegisterViewModel, User>();

            CreateMap<Token,TokenViewModel > ();
            CreateMap<User, RegisterViewModel>();
        }
    }
}
