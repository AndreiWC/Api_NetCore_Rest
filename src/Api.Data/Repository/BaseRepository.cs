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


        /*
        Recebe uma entidade, procura no banco de dados se existir no banco realiza o update
        se não encontrar retorna null e não mexe no banco 
        */
        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                //verifica se existe o registro no banco, se não existe retorna null
                var result = await _dataset.SingleOrDefaultAsync(p => p.Equals(item.Id));
                if (result == null)
                {
                    return null;
                }
                //atualizar a data de alteração do registro
                item.UpdateAt = DateTime.UtcNow;
                // mantem a data de criação do registro
                item.CreateAT = result.CreateAT;
                //context recebe os valores result e seta nele os valores alterados de item
                _context.Entry(result).CurrentValues.SetValues(item);
                //salva no banco, faz o commit ou o rolback no banco
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            // se tudo der certo retorna o item
            return item;
        }
    }
}