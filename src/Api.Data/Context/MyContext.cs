using System;
using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> User { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        // CONTEXTOFACTORY IRÁ PROVER A CONEXÃO PARA TEMPO DE DESGNER E O USERMAP CONFIGURA A TABELA NO BANCO DE DADOS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure); // MAPEIA A USERENTITY DO BANCO DE DADOS
            // AO RODAR A MIGRAÇÃO JÁ É GERADO UM NOVO USUÁRIO NO BANCO
            modelBuilder.Entity<UserEntity>().HasData(
                 new UserEntity
                 {
                     Id = Guid.NewGuid(),
                     Name = "Admin",
                     Email = "Admin@gmail.com",
                     UpdateAt = DateTime.Now,
                     CreateAT = DateTime.Now,

                 }

            );
        }

    }


}