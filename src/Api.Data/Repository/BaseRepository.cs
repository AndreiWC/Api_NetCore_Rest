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
        // Implementa o delete no banco via id do registro
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {    
                //recebe no result o valor retornado do banco        
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                //verifica se o result esta null, caso esteja sai da função
                if (result==null)
                {
                    return false;
                }
                //remove o resultado do banco
                _dataset.Remove(result);
                // realiza o commit da exclusão
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
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
 
        //Retorna o registro da tabela do banco de dados de acordo com o id passado
        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        // Retorna true se o registrio existe na tabela do banco de dados
        public async Task<bool> ExistAsync(Guid id)
        {
             return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }

        //Retorna uma lista com os registros da tabela (Select sem where)
        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
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