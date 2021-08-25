using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    /*
    IRespository É UMA INTERFACE QUE OBRIGA A IMPLMENTAÇÃO DOS METODOS A ONDE FOR HERDADA A CLASSE
    ELA É RESPONSAVEL POR CRIAR OS METODOS CRUD DAS ENTITIES
    ELA RECEBE UM OBJETO GENERICO<T> ONDE ELE DEVE SER DO TIPO BaseEntity
    ESSA INTERFACE UTILIZA TASKs PARA TRATAR A API POIS ELA PODE RECEBER METODOS SINCRONOS OU ASSINCRONOS
    */
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(Guid id);
        Task<T> SelectAsync(Guid id);
        Task<IEnumerable<T>> SelectAsync();
        Task<bool> ExistAsync(Guid id);

    }
}