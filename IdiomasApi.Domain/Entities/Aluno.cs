using System;
using System.Collections.Generic;
using System.Linq;
using IdiomasApi.Domain.ValueObjects;

namespace IdiomasApi.Domain.Entities
{
    public class Aluno
    {
        public Guid Id {  get; private set; }
        public string Nome { get; private set; }
        public Cpf Cpf { get; private set; }
        public Email Email { get; private set; }
        public Endereco Endereco { get; private set; }
        public ICollection<Turma> Turmas { get; private set; }

        protected Aluno()
        {
            Id = Guid.NewGuid();
            Nome = string.Empty;
            Cpf = null!;
            Email = null!;
            Endereco = null!;
            Turmas = new List<Turma>();
        }

        public Aluno(string nome, Cpf cpf, Email email, Endereco endereco, List<Turma> turmasIniciais)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do aluno é obrigatório.");

            if (turmasIniciais == null || !turmasIniciais.Any())
                throw new ArgumentException("O aluno deve ser vinculado a pelo menos uma turma no momento do cadastro.");
            
            Id = Guid.NewGuid();//Gera código númerico e de texto único de forma automática.
            Nome = nome.Trim();
            Cpf = cpf ?? throw new ArgumentException(nameof(cpf));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Endereco = endereco ?? throw new ArgumentNullException(nameof(endereco));

            Turmas = new List<Turma>(turmasIniciais);
        }
    }
}