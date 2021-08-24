using Microsoft.EntityFrameworkCore;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    // MAPEIA AS ENTITIES
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        // EJETA A ENTITY USERENTITY PARA O MEETODO CONFIGURE
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            //COM A ENTITY RECEBIDA PASSA OS COMANDOS PARA SER CONFIGURADA A TABELA
           // CRIA O NOME DA TABELA
            builder.ToTable("User");
            //CRIA O INDEX DA TABELA
            builder.HasKey(u => u.Id);
            //CRIA UM INDEX DA TABELA - CAMPO UNICO
            builder.HasIndex(u => u.Email).IsUnique();
            //CRIA O CAMPO NAME COM A PROPRIEDADE REQUERIDA(não pode ser null) E TAMANHO MAXIMO DE 60 CARCTERES
            builder.Property(u => u.Name).IsRequired().HasMaxLength(60);
            // CRIA O CAMPO EMAIL COM TAMANHO MAXIMO DE 100 CARACTERES
            builder.Property(u => u.Email).HasMaxLength(100);
            
        }
    }
}