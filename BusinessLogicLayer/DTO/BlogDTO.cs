using System.Collections.Generic;

namespace BusinessLogicLayer.DTO
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public List<PostDTO> Posts { get; set; }
    }
}
