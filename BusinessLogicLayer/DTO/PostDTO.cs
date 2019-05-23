using System.Collections.Generic;

namespace BusinessLogicLayer.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Rating { get; set; }

        List<CommentDTO> Comments { get; set; }
    }
}
