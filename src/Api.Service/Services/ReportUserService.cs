using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.Reports;
using AutoMapper;

namespace Api.Service.Services
{
    public class ReportUserService : IReportUserService
    {
        private IRepository<UserEntity> _repository;
        private readonly IMapper _mapper;

        public ReportUserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

 
        public async Task<IEnumerable<UserDto>> GetListagemSimples()
        {
            var listEntity = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UserDto>>(listEntity);
        }
    }
}