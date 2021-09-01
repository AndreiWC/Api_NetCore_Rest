using System;
using Api.CrossCutting.Mapping;
using AutoMapper;
using Xunit;

namespace Api.Service.Test
{
    public abstract class BaseTesetService
    {

        public IMapper Mapper { get; set; }

        public BaseTesetService()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new DtoToModelProfile());
                    cfg.AddProfile(new EntityToDtoProfile());
                    cfg.AddProfile(new ModelToEntityProfile());
                });
                return config.CreateMapper();
            }
            public void Dispose()
            {
            }
        }
    }
}
