using RecipeManagementApp.Context;
using RecipeManagementApp.Models.UserViewModels;
using AutoMapper;
using RecipeManagementApp.Context.Data;
using RecipeManagementApp.Models.RecipeViewModels.DTOs;

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
            CreateMap<User, ChangeRoleViewModel>().ReverseMap();
            CreateMap<Recipe, RecipeDTO>().ReverseMap();
        }
    }
}
