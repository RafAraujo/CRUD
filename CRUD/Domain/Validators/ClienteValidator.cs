using Domain.Models;
using FluentValidation;
using System;
using System.Linq;

namespace Domain.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.Cpf).NotNull().NotEmpty().Length(14);
            RuleFor(c => c.Nome).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(c => c.Rg).Length(12).When(c => !string.IsNullOrEmpty(c.Rg));
            RuleFor(c => c.DataExpedicao).GreaterThanOrEqualTo(DateTime.Today.AddYears(-150)).LessThanOrEqualTo(DateTime.Today).When(c => c.DataExpedicao != null);
            RuleFor(c => c.OrgaoExpedicao).MaximumLength(100);
            RuleFor(c => c.UfExpedicaoId).GreaterThan(0).When(c => c.UfExpedicaoId != null);
            RuleFor(c => c.DataNascimento).GreaterThanOrEqualTo(DateTime.Today.AddYears(-150)).LessThanOrEqualTo(DateTime.Today);
            RuleFor(c => c.Sexo).Must(sexo => new[] { "F", "M" }.Contains(sexo));

            RuleFor(c => c.Endereco).SetValidator(new EnderecoValidator());
        }
    }
}