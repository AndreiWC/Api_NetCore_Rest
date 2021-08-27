using Api.Domain.DTOs.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mapping
{
    public class DtoToModelProfile:Profile
    {
        //cria mapeamento da model e da userdto
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDto>()
            .ReverseMap();
        }
    }
}