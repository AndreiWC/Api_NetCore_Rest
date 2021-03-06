using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    //Implementa a interface do UserEntity que ira interagir com a camada de application
    public interface IUserService
    {
        Task<UserDto> Get(Guid Id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDtoCreateResult> Post(UserDtoCreate user);
        Task<UserDtoUpdateResult> Put(UserDtoUpdate user);
        Task<bool> Delete(Guid Id);

    }
}