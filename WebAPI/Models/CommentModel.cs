using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.TextRequired)]
        public string Text { get; set; }

        [Required(ErrorMessage = "The comment should refer to some post.")]
        public int PostId { get; set; }
    }
}
