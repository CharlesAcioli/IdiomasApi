using System.Text.RegularExpressions;

namespace IdiomasApi.Domain.ValueObjects
{
    public class Email
    {
        public string Endereco { get; private set; }

        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco) || !Validar(endereco))
                throw new ArgumentException("O E-mail informado é inválido.");

            Endereco = endereco.ToLower().Trim();
        }

        private static bool Validar(string email)
        {
            //regra universal que diz: "O texto precisa ter caracteres antes do @,
            //precisa ter o @, precisa ter caracteres depois e precisa ter um ponto".
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
    }
}