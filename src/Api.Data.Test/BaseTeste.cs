using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Test
{
    //cria uma base de teste para realizar os testes unitários
    public class BaseTeste
    {

        public BaseTeste()
        {

        }
    }

    public class DbTeste : IDisposable
    {
        // cria um nome de banco de dados para teste
        private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

        public ServiceProvider ServiceProvider { get; private set; }
        //cria um banco de dados para realizar os testex
        public DbTeste()
        {
            var serviceColletion = new ServiceCollection();
            serviceColletion.AddDbContext<MyContext>(o =>
                    o.UseMySql($"Persist Security Info=True;Server=localhost;Database={dataBaseName};User=root;Password=vssql"),
                     ServiceLifetime.Transient);

            ServiceProvider = serviceColletion.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }
        //após finalizar o teste unitário elimina o banco
        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
