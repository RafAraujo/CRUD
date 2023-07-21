using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    class EnderecoValidator : AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            RuleFor(e => e.Cep).NotNull().NotEmpty().Length(9);
            RuleFor(e => e.Logradouro).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(e => e.Numero).NotNull().NotEmpty().MaximumLength(10);
            RuleFor(e => e.Complemento).MaximumLength(10);
            RuleFor(e => e.Cidade).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(e => e.UfId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
