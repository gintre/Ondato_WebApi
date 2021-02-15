using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ondato_WebApi.Models.Dto
{
    public class CreateUpdateRequestDto : IValidatableObject
    {
        public string Key { get; set; }

        public CherryItemDto CherryItem { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CherryItem == null || string.IsNullOrEmpty(CherryItem.Title))
            {
                yield return new ValidationResult("You should provide cherry object with correct title");
            }

            if (string.IsNullOrEmpty(Key))
            {
                yield return new ValidationResult("You should provide object key");
            }
        }
    }
}
