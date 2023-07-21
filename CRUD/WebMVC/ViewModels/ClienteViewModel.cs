using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.ViewModels
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [DisplayName("CPF")]
        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string Cpf { get; set; }

        [StringLength(100)]
        [Required]
        public string Nome { get; set; }

        [DisplayName("RG")]
        [StringLength(12, MinimumLength = 12)]
        public string Rg { get; set; }

        [DisplayName("Data Expedição")]
        public DateTime? DataExpedicao { get; set; }

        [DisplayName("Órgão Expedição")]
        public string OrgaoExpedicao { get; set; }

        [DisplayName("UF Expedição")]
        public int? UfExpedicaoId { get; set; }

        public UfViewModel UfExpedicao { get; set; }

        [DisplayName("Data Nascimento")]
        [Required]
        public DateTime DataNascimento { get; set; }

        [DisplayName("Sexo")]
        [Required]
        public string Sexo { get; set; }

        [DisplayName("Estado Civil")]
        [Required]
        public string EstadoCivil { get; set; }

        public EnderecoViewModel Endereco { get; set; }
    }
}
