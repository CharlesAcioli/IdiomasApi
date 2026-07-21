using System;
using System.Collections.Generic;
using System.Linq;
using IdiomasApi.Domain.ValueObjects;

namespace IdiomasApi.Domain.Entities
{
    public class Turma
    {
        public Guid Id { get; private set; }
        public string NumeroTurma { get; private set; }
        public int AnoLetivo { get; private set; }
        public NivelTurma Nivel {  get; private set; }
        public Idioma IdiomaCurso { get; private set; }

        //Propriedade para ler os alunos matriculados nesta turma
        public ICollection<Aluno> Alunos { get; private set; }

        protected Turma()
        {
            Id = Guid.NewGuid();
            NumeroTurma = string.Empty;
            Alunos = new List<Aluno>();
        }

        public Turma(string numeroTurma, int anoLetivo, NivelTurma nivel, Idioma idiomacurso)
        {
            if (string.IsNullOrWhiteSpace(numeroTurma))
                throw new ArgumentException("O número da turma é obrigatório");

            if (anoLetivo < DateTime.Now.Year - 1)
                throw new ArgumentException("Ano letivo inválido");

            Id = Guid.NewGuid();
            NumeroTurma = numeroTurma.Trim();
            AnoLetivo = anoLetivo;
            Nivel = nivel;
            IdiomaCurso = idiomacurso;
            //Inicializa uma lista vazia para novos alunos poderem entrar depois.
            Alunos = new List<Aluno>();
        }

        public void MatricularAluno(Aluno aluno)
        {
            if (aluno == null)
                throw new ArgumentException(nameof(aluno));

            if (Alunos.Count >= 5)
                throw new InvalidOperationException($"A turma {NumeroTurma} já atingiu o limite de 5 alunos.");

            if (Alunos.Any(a => a.Id == aluno.Id || a.Cpf.Valor == aluno.Cpf.Valor))
                throw new InvalidOperationException($"O aluno {aluno.Nome} já está nesta turma.");

            Alunos.Add(aluno);
        }
    }
}