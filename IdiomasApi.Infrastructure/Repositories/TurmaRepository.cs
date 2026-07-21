using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdiomasApi.Domain.Entities;
using IdiomasApi.Infrastructure.Context;

namespace IdiomasApi.Infrastructure.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly ApplicationDbContext _context;

        public TurmaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Turma turma) => await _context.Turmas.AddAsync(turma);

        public async Task AtualizarAsync(Turma turma) => await Task.Run(() => _context.Turmas.Update(turma));

        public async Task ExcluirAsync(Turma turma) => await Task.Run(() => _context.Turmas.Remove(turma));

        public async Task<Turma> ObterPorIdAsync(Guid id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            return turma ?? throw new KeyNotFoundException($"Turma com o ID {id} não existe.");
        }

        public async Task<Turma> ObterComAlunosPorIdAsync(Guid id)
        {
            var turma = await _context.Turmas.Include(t => t.Alunos).FirstOrDefaultAsync(t => t.Id == id);
            return turma ?? throw new KeyNotFoundException($"Turma com os alunos vinculados não foi encontrada para o ID {id}.");

        }

        public async Task<IEnumerable<Turma>> ObterTodosAsync() => await _context.Turmas.ToListAsync();
    }
}