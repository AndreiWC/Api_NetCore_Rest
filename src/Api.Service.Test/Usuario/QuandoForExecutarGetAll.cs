using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
    public class QuandoForExecutarGetAll:UsuarioTestes
    {
                //CRIA UMA OBJ PRIVADO DA INTERFACE IUSERSERVICE E PASSA PARA A MOCK
        private IUserService _services;
        //A MOCK ALOCA NA MEMOMRIA O OBJETO IUSERSERVICE COM TODAS AS FUNÇÕES PARA REALIZAR A ENTRADA E A SAIDA DE DADOS 
        private Mock<IUserService> _serviceMock;

       [Fact(DisplayName="É Possível Executar o Método Get")]
        public async Task E_POSSIVEL_EXECUTAR_METODO_GETALL()
        {
            //INSTANCIA A MOCK
            _serviceMock = new Mock<IUserService>();
            //CONFIGURA A MOCK COM UM OBJETO CRIADO NA CLASSE USUARIOTESTE, RETORNA O OBJETE USERDTO POR MEIO DO GET DO OBJETO
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(listaDtoUser);
            //PASSA O OBJETO CRIADO PARA A SERVICE
            _services = _serviceMock.Object;

            //INICIO DOS TESTES - 1º
            var result  = await _services.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() == 10);


            var listaVazia = new List<UserDto>();
            
            //INSTANCIA OUTRO MOCK
            _serviceMock = new Mock<IUserService>();
            //CONFIGURA A MOCK COM UM OBJETO NULO CRIADO NA CLASSE USUARIOTESTE, RETORNA O OBJETE USERDTO POR MEIO DO GET DO OBJETO
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(listaVazia.AsEnumerable);
            //PASSA O OBJETO CRIADO PARA A SERVICE
            _services = _serviceMock.Object;

            //INICIO DOS TESTES - 2º
            var resultEmpty  = await _services.GetAll();
            Assert.Empty(resultEmpty);
            Assert.True(resultEmpty.Count() == 0);
        }


    
    }
}