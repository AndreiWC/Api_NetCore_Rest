using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarGet
{
    public class Retorno_Get
    {
        private UsersController _controller;

        [Fact(DisplayName = "É Possível Executar o Método Get")]

        public async Task E_Possivel_Invocar_A_Controller_Get()
        {

            //INSTANCIA A MOCK
            var _serviceMock = new Mock<IUserService>();
            //cria nome e email fake
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            //configura a mock, passa para o metodo get um guid qualquer e retorna uma dto
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(

                new UserDto
                {

                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                }


            );
            //instancia a usercontroller passando para ela o servicemock
            _controller = new UsersController(_serviceMock.Object);
            // recebe o resultado do get
            var result = await _controller.Get(Guid.NewGuid());

            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as UserDto;
            Assert.NotNull(resultValue);
            Assert.Equal(nome, resultValue.Name);
            Assert.Equal(email, resultValue.Email);



        }
    }
}