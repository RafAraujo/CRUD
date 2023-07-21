using FluentValidation;
using System.Web.Mvc;

namespace WebMVC.Controllers.Base
{
    public abstract class ValidatableController : Controller
    {
        protected virtual bool Validate<T>(T model, IValidator<T> validator)
        {
            var validationResult = validator.Validate(model);

            foreach (var validationFailure in validationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, validationFailure.ErrorMessage);
            }

            return validationResult.IsValid;
        }
    }
}