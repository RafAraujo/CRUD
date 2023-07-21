using Domain.Models.Base;

namespace Domain.Models
{
    public class Endereco : Entity
    {
        public int ClienteId { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public int UfId { get; set; }

        public Uf Uf { get; set; }
    }
}
