using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutarDelete : UsuarioTestes
    {
        //CRIA UMA OBJ PRIVADO DA INTERFACE IUSERSERVICE E PASSA PARA A MOCK
        private IUserService _services;
        //A MOCK ALOCA NA MEMOMRIA O OBJETO IUSERSERVICE COM TODAS AS FUNÇÕES PARA REALIZAR A ENTRADA E A SAIDA DE DADOS 
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É Possível Executar o Método Create")]
        public async Task E_POSSIVEL_EXECUTAR_METODO_DELETE()
        {
            //INSTANCIA A MOCK
            _serviceMock = new Mock<IUserService>();
            //CONFIGURA A MOCK COM UM OBJETO CRIADO NA CLASSE USUARIOTESTE 
            _serviceMock.Setup(m => m.Delete(IdUsuario)).ReturnsAsync(true);
            //PASSA O OBJETO CRIADO PARA A SERVICE
            _services = _serviceMock.Object;

            //INICIO DOS TESTES - 1º
            var result = await _services.Delete(IdUsuario);
            Assert.True(result);


            //INSTANCIA A MOCK
            _serviceMock = new Mock<IUserService>();
            //CONFIGURA A MOCK COM UM OBJETO CRIADO NA CLASSE USUARIOTESTE 
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            //PASSA O OBJETO CRIADO PARA A SERVICE
            _services = _serviceMock.Object;

            result = await _services.Delete(Guid.NewGuid());
            Assert.False(result);





        } 
    }
}