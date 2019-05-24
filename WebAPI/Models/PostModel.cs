using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class PostModel : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = Constants.TitleRequired)]
        public string Title { get; set; }
        [Required(ErrorMessage = Constants.ContentRequired)]
        public string Content { get; set; }
        [Required(ErrorMessage = Constants.DateRequired)]
        public DateTime Date { get; set; }

        public int? Rating { get; set; }
        public int BlogId { get; set; }
        public BlogModel Blog { get; set; }
        public List<CommentModel> Comments { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date > DateTime.UtcNow)
            {
                yield return new ValidationResult(Constants.DateInvalid, new List<string> {"Date"});
            }
        }
    }
}
