using RecipeManagementApp.Context;
using RecipeManagementApp.Models.UserViewModel;
using AutoMapper;

namespace RecipeManagementApp.AutoMapperProfiles
{
    public class UserProfiles :Profile
    {
        public UserProfiles() 
        {
            CreateMap<User, IndexUserViewModel>().ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
            CreateMap<User, EditUserViewModel>().ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
            CreateMap<User, DeleteUserViewModel>().ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
            CreateMap<User, ChangePasswordUserViewModel>().ReverseMap();
        }
    }
}
