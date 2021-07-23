using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TruckRegistration.Domain
{
    /// <summary>
    /// This class will act as a Data Transfer Object to the appliation and the domain layers.
    /// Here it acts as both:
    ///   - A view model - receiving data from the client and sending to the domain.
    ///   - A binding model - used to send data from the domain to the client.
    /// It also has the custom validation and corresponding error messages required by this project.
    /// </summary>
    public class TruckViewModel : IValidatableObject
    {
        /// <summary>
        /// The id of the truck, if bind by the application. It should not be sent from the client.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Truck Model. For this solution, it may only be FH or FM.
        /// </summary>
        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "{0} deve ser preenchido.")]
        public string Model { get; set; }

        /// <summary>
        /// Truck Production Year, which should be limited to the current year.
        /// </summary>
        [Display(Name = "Ano de Fabricação")]
        [Required(ErrorMessage = "{0} deve ser preenchido.")]
        [Range(2000, 2099, ErrorMessage = "{0} fora do intervalo permitido.")]
        public int ProductionYear { get; set; }

        /// <summary>
        /// Truck Model Year, being either the current or the following year.
        /// </summary>
        [Range(2000, 2099, ErrorMessage = "{0} fora do intervalo permitido.")]
        [Display(Name = "Ano Modelo")]
        [Required(ErrorMessage = "{0} deve ser preenchido.")]
        public int ModelYear { get; set; }

        /// <summary>
        /// A method to produce a readable string output to the model, eg 'Truck 1: FM 2020/2021'
        /// </summary>
        public override string ToString()
        {
            return $"Truck {Id}: {Model} {ProductionYear}/{ModelYear}";
        }

        /// <summary>
        /// The validation implementation considering the projects constrands.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A list of yielded error validation messages.</returns>
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
