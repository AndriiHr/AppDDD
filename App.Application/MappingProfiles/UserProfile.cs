using App.Application.Users;
using App.Application.Users.Commands;
using App.Domain.Users;
using AutoMapper;
using System.Collections.Generic;

namespace App.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<List<User>, List<UserDto>>();
            CreateMap<List<UserDto>, List<User>>();

            CreateMap<RegisterUserRequest, UserDto>();
        }
    }
}