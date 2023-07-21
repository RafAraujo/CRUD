using Domain.Models.Base;
using System;

namespace Domain.Models
{
    public class Cliente : Entity
    {
        public string Cpf { get; set; }

        public string Nome { get; set; }

        public string Rg { get; set; }

        public DateTime? DataExpedicao { get; set; }

        public string OrgaoExpedicao { get; set; }

        public int? UfExpedicaoId { get; set; }

        public Uf UfExpedicao { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }

        public string EstadoCivil { get; set; }

        public Endereco Endereco { get; set; }
    }
}
