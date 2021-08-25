using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.User
{
    //Implementa a interface do UserEntity que ira interagir com a camada de application
    public interface IUserService
    {
        Task<UserEntity> Get(Guid Id);
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> Post(UserEntity user);
        Task<UserEntity> Put(UserEntity user);
        Task<bool> Delete(Guid Id);

    }
}