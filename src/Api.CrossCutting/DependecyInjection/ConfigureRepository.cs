using Api.Data.Repository;
using Api.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependecyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceColletion){
            serviceColletion.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        }
    }
}