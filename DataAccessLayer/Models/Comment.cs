using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Comment: IIdentity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
