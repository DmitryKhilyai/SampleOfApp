using BusinessLogicLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IPostService
    {
        Task CreatePostAsync(PostDTO dTO);
        Task<PostDTO> GetPostAsync(int? id);
        Task<IEnumerable<PostDTO>> GetPostAsync();
        Task ChangePostAsync(PostDTO dTO);
        Task DeletePostAsync(int id);
    }
}
