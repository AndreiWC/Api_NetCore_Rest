using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FastReport.Data;
using System;

namespace Api.CrossCutting.DependecyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceColletion)
        {


            serviceColletion.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceColletion.AddScoped<IUserRepository, UserImplementation>();
            //TRATAMENTO FEITO PARA VERIFICAR SE A CONEXÃO É DO SQLSERVER OU MYSQL
            // PEGA A CONEXÃO VIA PARAMETRO CONFIGURADO NO LAUNCH.JSON

            // CONFIGURA O FASTREPORT
            FastReport.Utils.RegisteredObjects.AddConnection(typeof(MySqlDataConnection));

            if (Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower())
            {
                serviceColletion.AddDbContext<MyContext>(
                    options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION")));
            }
            else
            {
                serviceColletion.AddDbContext<MyContext>(
                    options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION")));
            }
        }
    }
}