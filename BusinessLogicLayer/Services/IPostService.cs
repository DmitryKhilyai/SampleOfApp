using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
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
