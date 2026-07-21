using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdiomasApi.Domain.Entities
{
    public interface ITurmaRepository
    {
        Task AdicionarAsync(Turma turma);
        Task AtualizarAsync(Turma turma);
        Task ExcluirAsync(Turma turma);
        Task<Turma> ObterPorIdAsync(Guid id);

        Task<Turma> ObterComAlunosPorIdAsync(Guid id);
        Task<IEnumerable<Turma>> ObterTodosAsync();
    }
}