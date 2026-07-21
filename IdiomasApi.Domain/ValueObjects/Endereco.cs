namespace IdiomasApi.Domain.ValueObjects
{
    public class Endereco
    {
        public string Valor { get; private set; }

        public Endereco(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("O endereco não pode estar em branco.");

            Valor = valor.Trim();
        }
    }
}