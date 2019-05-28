using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Blog: IIdentity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        public List<Post> Posts { get; set; }
    }
}
