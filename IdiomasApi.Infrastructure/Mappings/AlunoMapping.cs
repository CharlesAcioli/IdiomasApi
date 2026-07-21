using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdiomasApi.Domain.Entities;
using IdiomasApi.Domain.ValueObjects;

namespace IdiomasApi.Infrastructure.Mappings
{
    //Manual de instruções para configurar a tabela da Class(Aluno)
    public class AlunoMapping : IEntityTypeConfiguration<Aluno>
    {
        //builder -> variável que dita as regras da tabela (ex: Colunas, Linhas, Chaves primárias, etc...)
        //Método obrigatório e exigido.
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            //Define o nome da tabela no banco de dados MS SQL Server
            builder.ToTable("Alunos");
            //Define a chave primária
            builder.HasKey(a => a.Id);
            //Uso de lambda, caso precise renomear(ex: Nome -> NomeCompleto)
            //Define que o campo é obrigatório e tem tamanho máximo 150 caracteres
            builder.Property(a => a.Nome)//Poderia usar builder.Property("Nome"); -> Mas não conseguiria trocar, se trocasse quebraria todo o código.
                .IsRequired()
                .HasColumnType("VARCHAR(150)");

            builder.Property(a => a.Cpf)
                .IsRequired()
                .HasColumnType("VARCHAR(11)")
                .HasConversion(
                    cpfObjeto => cpfObjeto.Valor,
                    cpfTexto => new IdiomasApi.Domain.ValueObjects.Cpf(cpfTexto)
                 );

            builder.Property(a => a.Email)
                .IsRequired()
                .HasColumnType("VARCHAR(100)")
                .HasConversion(
                    emailObjeto => emailObjeto.Endereco,
                    emailTexto => new IdiomasApi.Domain.ValueObjects.Email(emailTexto)
                 );

            builder.Property(a => a.Endereco)
                .IsRequired()
                .HasColumnType("VARCHAR(150)")
                .HasConversion(
                enderecoObjeto => enderecoObjeto.Valor,
                enderecoTexto => new
                IdiomasApi.Domain.ValueObjects.Endereco(enderecoTexto)
                );

            builder.HasMany(a => a.Turmas)
                .WithMany(t => t.Alunos)
                .UsingEntity<Dictionary<string, object>>(
                "AlunoTurma",
                j => j.HasOne<Turma>().WithMany().HasForeignKey("TurmaId"),
                j => j.HasOne<Aluno>().WithMany().HasForeignKey("AlunoId")
                );
        }
    }
}