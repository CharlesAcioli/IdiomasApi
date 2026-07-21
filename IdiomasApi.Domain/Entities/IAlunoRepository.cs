using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdiomasApi.Domain.Entities
{
    public interface IAlunoRepository
    {
        Task AdicionarAsync(Aluno aluno);
        Task AtualizarAsync(Aluno aluno);
        Task ExcluirAsync(Aluno aluno);
        Task<Aluno> ObterPorIdAsync(Guid id);
        Task<Aluno> ObterPorCpfAsync(string cpf);
        Task<IEnumerable<Aluno>> ObterTodosAsync();
    }
}