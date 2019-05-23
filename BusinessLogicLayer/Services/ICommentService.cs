using BusinessLogicLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface ICommentService
    {
        Task CreateCommentAsync(CommentDTO dTO);
        Task<CommentDTO> GetCommentAsync(int? id);
        Task<IEnumerable<CommentDTO>> GetCommentsAsync();
        Task ChangeCommentAsync(CommentDTO dTO);
        Task DeleteCommentAsync(int id);
    }
}
