using System;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
    public class QuandoForExecutarFindByLogin


    {

        //CRIA UMA OBJ PRIVADO DA INTERFACE IUSERSERVICE E PASSA PARA A MOCK
        private ILoginService _services;
        //A MOCK ALOCA NA MEMOMRIA O OBJETO IUSERSERVICE COM TODAS AS FUNÇÕES PARA REALIZAR A ENTRADA E A SAIDA DE DADOS 
        private Mock<ILoginService> _serviceMock;
        [Fact(DisplayName = "É Possível executar o Método FindByLogin")]

        public async Task E_POSSIVEL_EXECUTAR_METODO_FINDBYLOGIN()
        {

            var email = Faker.Internet.Email();

            var objetoRetorno = new
            {

                authenticated = true,
                create = DateTime.UtcNow,
                expiration = DateTime.UtcNow.AddHours(8),
                accessToken = Guid.NewGuid(),
                userName = email,
                name = Faker.Name.FullName(),
                message = "Usuário Logado com sucesso"

            };

            var loginDto = new LoginDto
            {
                Email = email
            };

            //CRIAR O MOCK DA APLICAÇÃO
            _serviceMock = new Mock<ILoginService>();
            //CONFIGURA O MOCK
            _serviceMock.Setup(M => M.FindByLogin(loginDto)).ReturnsAsync(objetoRetorno);
            //RECEBE  O OBJETO MOCK
            _services = _serviceMock.Object;

            var result = await _services.FindByLogin(loginDto);
            Assert.NotNull(result);


        }
    }
}