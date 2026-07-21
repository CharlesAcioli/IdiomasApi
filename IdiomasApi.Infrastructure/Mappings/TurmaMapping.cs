using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdiomasApi.Domain.Entities;

namespace IdiomasApi.Infrastructure.Mappings
{
    public class TurmaMapping : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.NumeroTurma)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder.Property(t => t.AnoLetivo)
                .IsRequired()
                .HasColumnType("INT");

            builder.Property(t => t.Nivel)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnType("INT");

            builder.Property(t => t.IdiomaCurso)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnType("INT");
        }
    }
}