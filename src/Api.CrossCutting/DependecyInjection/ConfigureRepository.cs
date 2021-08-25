using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependecyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            serviceColletion.AddDbContext<MyContext>(
                options => options.UseMySql("Server=Localhost;Port=3306;Database=dbApi;Uid=root;Pwd=vssql")
            );
        }
    }
}