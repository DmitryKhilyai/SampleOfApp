using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public int? Rating { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
