using BusinessLogicLayer.DataValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTO
{
    public class CommentDTO : IValidatableObject
    {
        [NonNegativeInteger(ErrorMessage = Constants.IdentifierInvalid)]
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.TextRequired)]
        public string Text { get; set; }

        [Required(ErrorMessage = "The comment should refer to some post.")]
        public int PostId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Id < 0)
            {
                yield return new ValidationResult(Constants.IdentifierInvalid, new List<string> { "id" });
            }
        }
    }
}
