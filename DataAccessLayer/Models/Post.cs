using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int? Rating { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
