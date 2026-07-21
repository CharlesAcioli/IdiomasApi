using Microsoft.EntityFrameworkCore; //Importa o banco de dados(Biblioteca do Entity).
using IdiomasApi.Domain.Entities; //Importando a pasta Domain>Entities(Onde estão as classes)
using IdiomasApi.Infrastructure.Mappings;

namespace IdiomasApi.Infrastructure.Context
{
    //O símbolo ":" significa herança, ou seja, ApplicationDbContext herda tudo do DbContext original da Microsoft. Será um "gerente" do SQL.
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; } //Aviso ao Entity: Gere uma tabela Alunos no SQL Server.
        public DbSet<Turma> Turmas { get; set; }//"               " "               Turma               "

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoMapping());

            modelBuilder.ApplyConfiguration(new TurmaMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}