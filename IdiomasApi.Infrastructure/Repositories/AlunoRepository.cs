using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdiomasApi.Domain.Entities;
using IdiomasApi.Infrastructure.Context;

namespace IdiomasApi.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly ApplicationDbContext _context;

        public AlunoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Aluno aluno) => await _context.Alunos.AddAsync(aluno);

        public async Task AtualizarAsync(Aluno aluno) => await Task.Run(() => _context.Alunos.Update(aluno));

        public async Task ExcluirAsync(Aluno aluno) => await Task.Run(() => _context.Alunos.Remove(aluno));

        public async Task<Aluno> ObterPorIdAsync(Guid id)
        {
            var aluno = await _context.Alunos.Include(a => a.Turmas).FirstOrDefaultAsync(a => a.Id == id);
            return aluno ?? throw new KeyNotFoundException($"Aluno com o ID {id} não foi econtrado no banco de dados.");

            //=>
            //await _context.Alunos.Include(a => a.Turmas).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Aluno> ObterPorCpfAsync(string cpf)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Cpf.Valor == cpf);
            return aluno ?? throw new KeyNotFoundException($"Nenhuma aluno com o CPF informado foi encontrado.");
        }

        public async Task<IEnumerable<Aluno>> ObterTodosAsync() => await _context.Alunos.ToListAsync();
    }
}