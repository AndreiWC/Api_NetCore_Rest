using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;

namespace Api.Domain.Interfaces.Services.Reports
{
    public interface IReportUserService
    {
        Task<IEnumerable<UserDto>> GetListagemSimples();
 
    }
}