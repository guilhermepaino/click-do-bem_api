using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SantaHelena.ClickDoBem.Application.AppViewModels;
using Microsoft.AspNetCore.Authorization;

namespace SantaHelena.ClickDoBem.Services.Api.Controllers
{

    [Produces("application/json")]
    public abstract class CdbApiControllerBase : ControllerBase
    {
        protected new IActionResult Response(object res)
        {
            return Ok(new
            {
                success = true,
                data = res
            });
        }

        protected new IActionResult Response<TViewModel>(object result = null) where TViewModel : ViewModelBase
        {
            var errorMessages = new List<string>();

            if (result != null && result.GetType() == typeof(TViewModel))
            {
                ((TViewModel)result)
                    .ValidationResult
                    .Errors.ToList()
                    .ForEach(e => errorMessages.Add(e.ErrorMessage));
            }

            if (!errorMessages.Any() && ModelState.IsValid)
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            ModelState
                .Values
                .SelectMany(v => v.Errors).ToList()
                .ForEach(error =>
                {
                    var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                    errorMessages.Add(errorMsg);
                });

            return BadRequest(new
            {
                success = false,
                errors = errorMessages.Distinct().ToList()
            });
        }
    }

}