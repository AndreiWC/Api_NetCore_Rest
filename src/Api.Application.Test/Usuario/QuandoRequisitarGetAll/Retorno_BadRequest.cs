using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarGetAll
{
    public class Retorno_Badrequest
    {
        private UsersController _controller;

        [Fact(DisplayName = "É Possível Executar o Método Get")]

        public async Task E_Possivel_Invocar_A_Controller_GetAll()
        {

            //INSTANCIA A MOCK
            var _serviceMock = new Mock<IUserService>();
            //cria nome e email fake
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();
            //configura a mock, passa para o metodo get um guid qualquer e retorna uma dto
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
               new List<UserDto>{

                new UserDto
                {

                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                },

                new UserDto
                {

                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                }
                }

            );

            //instancia a usercontroller passando para ela o servicemock
            _controller = new UsersController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Forma Inválido");
            // recebe o resultado do get
            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
 



        }
    }
}

