using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class BlogModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public List<PostModel> Posts { get; set; }
    }
}
