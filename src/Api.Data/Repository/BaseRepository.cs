using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRespository<T> where T : BaseEntity
    {
        /*CONTRUTOR CRIADO PARA RECEBER O CONTEXT POR EJEÇÃO AO INICIAR A APLICAÇÃO*/
        protected readonly MyContext _context;
        private DbSet<T> _dataset;
        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();

        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        // Implementação de Insert no database

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                //verifica se o id do context esta vazio se sim preenche com um novo Guid
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                //passa para o CreateAT a dataatual
                item.CreateAT = DateTime.UtcNow;
                //salva o context no banco de dados
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return item;
        }

        public Task<T> SelectAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> SelectAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}