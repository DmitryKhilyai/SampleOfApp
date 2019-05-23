using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Text field is required")]
        public string Text { get; set; }
    }
}
