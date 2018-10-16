using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace SantaHelena.ClickDoBem.Application.Dto
{
    public class ViewModelBase
    {

        public ViewModelBase()
        {
            ValidationResult = new FluentValidation.Results.ValidationResult();
        }

        [ScaffoldColumn(false)]
        public FluentValidation.Results.ValidationResult ValidationResult { get; set; }

        [ScaffoldColumn(false)]
        public CascadeMode CascadeMode { get; set; }

    }
}
