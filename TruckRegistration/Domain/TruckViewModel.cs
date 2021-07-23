using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TruckRegistration.Domain
{
    public class TruckViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "{0} deve ser preenchido.")]
        public string Model { get; set; }

        [Display(Name = "Ano de Fabricação")]
        [Required(ErrorMessage = "{0} deve ser preenchido.")]
        [Range(2000, 2099, ErrorMessage = "{0} fora do intervalo permitido.")]
        public int ProductionYear { get; set; }

        [Range(2000, 2099, ErrorMessage = "{0} fora do intervalo permitido.")]
        [Display(Name = "Ano Modelo")]
        [Required(ErrorMessage = "{0} deve ser preenchido.")]
        public int ModelYear { get; set; }

        public override string ToString()
        {
            return $"Truck {Id}: {Model} {ProductionYear}/{ModelYear}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Model != "FH" && Model != "FM")
            {
                yield return new ValidationResult("Modelo apenas aceita FH e FM.", new[] { nameof(Model) });
            }

            if (ProductionYear != DateTime.Now.Year)
            {
                yield return new ValidationResult("Ano de Fabricação somente deve ser o atual.", new[] { nameof(ProductionYear) });
            }

            if (ModelYear > DateTime.Now.Year + 1 || ModelYear < DateTime.Now.Year)
            {
                yield return new ValidationResult("Ano Modelo somente deve ser o atual ou subsequente.", new[] { nameof(ModelYear) });
            }
        }
    }
}
