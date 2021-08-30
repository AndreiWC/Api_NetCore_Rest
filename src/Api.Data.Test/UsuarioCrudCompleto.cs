using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTeste, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_CRUD_Usuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                // cria o novo banco por enjeção de dependencias
                UserImplementation _repositorio = new UserImplementation(context);
                // cria uma nova entity para inserir no banco
                UserEntity _entity = new UserEntity
                {
                    //preenche o obj com os dados para teste
                    Email = Faker.Internet.Email(), //BIBLIOTECA QUE GERAR EMAILS FAKES
                    Name = Faker.Name.FullName()//BIBLIOTECA QUE GERAR NOMES FAKES
                };

                // TESTE DE INSERÇÃO DE DADOS

                // passa a entity para o o metodo de inserção do _repositorio
                var _registroCriado = await _repositorio.InsertAsync(_entity);
                //por meio do Assert verifica se o objeto esta null
                Assert.NotNull(_repositorio);
                // por meio do Assert verifica se o email é igual
                Assert.Equal(_entity.Email, _registroCriado.Email);
                //por meio do Assert verifica se o name do usuario é igual
                Assert.Equal(_entity.Name, _registroCriado.Name);
                //por meio do Assert verifica o id não é igual a um novo Guid 
                Assert.False(_registroCriado.Id == Guid.Empty);

                // TESTE DE ATUALIZAÇÃO DE DADOS

                _entity.Name = Faker.Name.First(); //Pega apenas o primeiro nome do usuario
                var _registroAtualizado = await _repositorio.UpdateAsync(_entity);
                //por meio do Assert verifica se o objeto esta null
                Assert.NotNull(_registroAtualizado);
                // por meio do Assert verifica se o email é igual
                Assert.Equal(_entity.Email, _registroAtualizado.Email);
                //por meio do Assert verifica se o name do usuario é igual
                Assert.Equal(_entity.Name, _registroAtualizado.Name);

                //TESTE DE EXISTENCIA

                var _registroExiste = await _repositorio.ExistAsync(_registroAtualizado.Id); // Seleciona o registro pelo ID do Objeto atualizado
                //por meio do Assert verifica se o objeto do registro existe
                Assert.True(_registroExiste);

                //TESTE DE SELEÇÃO
                var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id); // Seleciona o registro pelo ID do Objeto atualizado
                //por meio do Assert verifica se o objeto esta null
                Assert.NotNull(_registroSelecionado);
                // por meio do Assert verifica se o email é igual ao selecionado
                Assert.Equal(_registroAtualizado.Email, _registroSelecionado.Email);
                //por meio do Assert verifica se o name do usuario é igual ao selecionado
                Assert.Equal(_registroAtualizado.Name, _registroSelecionado.Name);


                //TESTE DE SELECIONAR TODOS OS REGISTROS
                var _todosRegistros = await _repositorio.SelectAsync();
                //por meio do Assert verifica se o objeto esta null
                Assert.NotNull(_todosRegistros);
                //por meio do Assert verifica se existe mais de um registro no banco 
                Assert.True(_todosRegistros.Count() > 1);

                // TESTE DE DELETE DO BANCO
                var _removeu = await _repositorio.DeleteAsync(_registroSelecionado.Id);
                Assert.True(_removeu);

                // TESTE DE USUARIO DE LOGIN
                var _usuarioPadrao = await _repositorio.FindByLogin("Admin@gmail.com");
                Assert.NotNull(_usuarioPadrao);
                Assert.Equal("Admin@gmail.com", _usuarioPadrao.Email);
                Assert.Equal("Admin", _usuarioPadrao.Name);






                // caso o teste seja bem sucedido o banco de teste criado é deletado 
            }
        }

    }
}