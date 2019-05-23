using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Rating { get; set; }

        public List<CommentModel> Comments { get; set; }
    }
}
