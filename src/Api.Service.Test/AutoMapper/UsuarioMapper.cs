using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTesetService
    {
        [Fact(DisplayName = "É possível Mapear os Modelos")]

        public void E_POSSIVEL_MAPEAR_OS_MODELOS()
        {
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow

            };
            var listEntity = new List<UserEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAT = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow

                };

                listEntity.Add(item);

            }
            //MODEL => ENTITY -- TRANSFORMA MODEL PARA ENTITY
            var dtoToEntity = Mapper.Map<UserEntity>(model);
            Assert.Equal(dtoToEntity.Id, model.Id);
            Assert.Equal(dtoToEntity.Name, model.Name);
            Assert.Equal(dtoToEntity.Email, model.Email);
            Assert.Equal(dtoToEntity.CreateAT, model.CreateAt);
            Assert.Equal(dtoToEntity.UpdateAt, model.UpdateAt);


            //ENTITY => DTO-- TRANSFORMA ENTITY PARA DTO
            var userDto = Mapper.Map<UserDto>(dtoToEntity);
            Assert.Equal(userDto.Id, dtoToEntity.Id);
            Assert.Equal(userDto.Name, dtoToEntity.Name);
            Assert.Equal(userDto.Email, dtoToEntity.Email);
            Assert.Equal(userDto.CreateAt, dtoToEntity.CreateAT);


            var listaDto = Mapper.Map<List<UserDto>>(listEntity);
            Assert.True(listaDto.Count() == listEntity.Count());
            for (int i = 0; i < listaDto.Count(); i++)
            {
                Assert.Equal(listaDto[i].Id, listEntity[i].Id);
                Assert.Equal(listaDto[i].Name, listEntity[i].Name);
                Assert.Equal(listaDto[i].Email, listEntity[i].Email);
                Assert.Equal(listaDto[i].CreateAt, listEntity[i].CreateAT);

            }





        }

    }
}