using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequistarDelete
{
    public class Retorno_Badrequest
    {
        private UsersController _controller;

        [Fact(DisplayName = "É Possível Executar o Método Delete")]

        public async Task E_Possivel_Invocar_A_Controller_Delete()
        {

            //INSTANCIA A MOCK
            var _serviceMock = new Mock<IUserService>();

        


            //CONFIGURA A MOCK COM UM OBJETO CRIADO NA CLASSE USUARIOTESTE, RETORNA O OBJETE userDtoCreate POR MEIO DO Post DO OBJETO
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _controller = new UsersController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato invalido");


            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
 

        }

    }
}