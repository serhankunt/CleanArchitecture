using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Utilities;
public sealed class ValidationAttribute<TValidator> : Attribute, IAsyncActionFilter
    where TValidator : class, IValidator
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var serviceProvider = context.HttpContext.RequestServices;
        var validatorType = typeof(IValidator<>).MakeGenericType(context.ActionArguments.First().Value.GetType());
        var validator = serviceProvider.GetService(validatorType) as IValidator;

        if (validator != null)
        {
            var validationResult = await validator.ValidateAsync(new ValidationContext<object>(context.ActionArguments.First().Value));

            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                context.Result = new BadRequestObjectResult(errorMessage);
                return;
            }
        }

        await next();
    }
}


