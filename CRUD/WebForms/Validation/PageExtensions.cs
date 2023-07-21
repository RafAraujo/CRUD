using FluentValidation;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebForm.Validation
{
    public static class PageExtensions
    {
        public static bool Validar<T>(this System.Web.UI.Page page, IValidator<T> validator, T model)
        {
            var validationResult = validator.Validate(model);

            foreach (var error in validationResult.Errors)
            {
                AdicionarErroValidacao(page, error.ErrorMessage);
            }

            return page.Validators.Cast<BaseValidator>().All(v => v.IsValid);
        }

        public static void AdicionarErroValidacao(this System.Web.UI.Page page, Exception ex)
        {
            AdicionarErroValidacao(page, ex.Message);
        }

        public static void AdicionarErroValidacao(this System.Web.UI.Page page, string message)
        {
            var validator = new CustomValidator
            {
                IsValid = false,
                ErrorMessage = message
            };
            page.Validators.Add(validator);
        }
    }
}