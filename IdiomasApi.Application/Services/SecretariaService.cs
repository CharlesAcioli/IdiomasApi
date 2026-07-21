using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdiomasApi.Domain.Entities;
using IdiomasApi.Domain.ValueObjects;

namespace IdiomasApi.Application.Services
{
    public class SecretariaService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;

        public SecretariaService(IAlunoRepository alunoRepository, ITurmaRepository turmaRepository)
        {
            _alunoRepository = alunoRepository ?? throw new ArgumentNullException(nameof(alunoRepository));
            _turmaRepository = turmaRepository ?? throw new ArgumentNullException(nameof(turmaRepository));
        }
        #region OPERAÇÕES DE ALUNO (Secretaria no Balcão)

        //Cadastra e matricula um novo aluno.
        public async Task MatricularNovoAlunoAsync(string nome, string cptTexto, string emailTexto, string enderecoTexto, Guid turmaIdInicial)
        {
            var turma = await _turmaRepository.ObterComAlunosPorIdAsync(turmaIdInicial);
            if (turma == null)
                throw new ArgumentException("A turma selecionada para a matrícula não existe.");

            if (turma.AnoLetivo < DateTime.Now.Year)
                throw new InvalidOperationException("Não é permitido matricular alunos em turmas de anos letivos passados.");

            var cpf = new Cpf(cptTexto);
            var email = new Email(emailTexto);
            var endereco = new Endereco(enderecoTexto);

            var alunoExistente = await _alunoRepository.ObterPorCpfAsync(cpf.Valor);
            if (alunoExistente != null)
                throw new InvalidOperationException("Já existe um aluno cadastrado com este CPF no sistema.");

            var turmasIniciais = new List<Turma> { turma };
            var novoAluno = new Aluno(nome, cpf, email, endereco, turmasIniciais);

            turma.MatricularAluno(novoAluno);

            await _alunoRepository.AdicionarAsync(novoAluno);
            await _turmaRepository.AtualizarAsync(turma);
        }

        public async Task CancelarCadastroAlunoAsync(Guid alunoId)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(alunoId);
            if (aluno == null)
                throw new ArgumentException("Aluno não encontrado.");

            if (aluno.Turmas != null && aluno.Turmas.Count > 0)
                throw new InvalidOperationException("Não é permitido excluir um aluno que possui matrículas ativas em trumas.");

            await _alunoRepository.ExcluirAsync(aluno);
        }

        #endregion

        #region OPERAÇÕES DE TURMA (Secretaria Organizando as Salas)

        public async Task CriarNovaTurmaAsync(string numeroTurma, int anoLetivo, NivelTurma nivel, Idioma idiomacurso)
        {
            if (anoLetivo < DateTime.Now.Year)
                throw new ArgumentException("Não é permitido criar turmas para anos letivos passados.");

            var novaTurma = new Turma(numeroTurma, anoLetivo, nivel, idiomacurso);
            await _turmaRepository.AdicionarAsync(novaTurma);
        }

        public async Task FecharTurmaAsync(Guid turmaId)
        {
            var turma = await _turmaRepository.ObterComAlunosPorIdAsync(turmaId);
            if (turma == null)
                throw new ArgumentException("Turma não encontrada.");

            if (turma.Alunos != null && turma.Alunos.Count > 0)
                throw new InvalidOperationException("Não é permitido fechar ou excluir uma turma que possui alunos matriculados.");

            await _turmaRepository.ExcluirAsync(turma);
        }

        #endregion
    }
}