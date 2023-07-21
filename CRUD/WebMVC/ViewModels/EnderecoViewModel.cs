using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.ViewModels
{
    public class EnderecoViewModel
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        [DisplayName("CEP")]
        [Required]
        [StringLength(9, MinimumLength = 9)]
        public string Cep { get; set; }

        [Required]
        [StringLength(100)]
        public string Logradouro { get; set; }

        [DisplayName("Número")]
        [Required]
        [StringLength(10)]
        public string Numero { get; set; }

        [StringLength(100)]
        public string Complemento { get; set; }

        [Required]
        [StringLength(100)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(100)]
        public string Cidade { get; set; }

        [DisplayName("UF")]
        [Required]
        public int UfId { get; set; }

        public UfViewModel Uf { get; set; }

        [DisplayName("Endereço")]
        public string EnderecoCompleto
        {
            get => $"{Logradouro}, {Numero} - {Cidade} - {Uf.Sigla}";
        }
    }
}