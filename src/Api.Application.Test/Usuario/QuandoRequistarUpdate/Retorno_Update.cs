using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequistarUpdate
{
    public class Retorno_Update
    {
        private UsersController _controller;

        [Fact(DisplayName = "É Possível Executar o Método Update")]

        public async Task E_Possivel_Invocar_A_Controller_Update()
        {

            //INSTANCIA A MOCK
            var _serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            //CONFIGURA A MOCK COM UM OBJETO CRIADO NA CLASSE USUARIOTESTE, RETORNA O OBJETE userDtoCreate POR MEIO DO Post DO OBJETO
            _serviceMock.Setup(m => m.Put(It.IsAny<UserDtoUpdate>())).ReturnsAsync(

                new UserDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    UpdateAt = DateTime.UtcNow

                }
            );
            _controller = new UsersController(_serviceMock.Object);

            var userDtoUpdate = new UserDtoUpdate
            {
                Id = Guid.NewGuid(),
                Name = nome,
                Email = email

            };

            var result = await _controller.Put(userDtoUpdate);
            Assert.True(result is OkObjectResult);

            UserDtoUpdateResult resultValue = ((OkObjectResult)result).Value as UserDtoUpdateResult;
            Assert.NotNull(resultValue);
            Assert.Equal(userDtoUpdate.Name, resultValue.Name);
            Assert.Equal(userDtoUpdate.Email, resultValue.Email);

        }

    }
}