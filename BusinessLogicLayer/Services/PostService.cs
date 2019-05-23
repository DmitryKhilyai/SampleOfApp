using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Post> _repository;

        public PostService(IMapper mapper, IRepository<Post> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task ChangePostAsync(PostDTO dTO)
        {
            throw new NotImplementedException();
        }

        public Task CreatePostAsync(PostDTO dTO)
        {
            throw new NotImplementedException();
        }

        public Task DeletePostAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PostDTO> GetPostAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var item = await _repository.GetByIdAsync(id.Value);
            return _mapper.Map<Post, PostDTO>(item);
        }

        public async Task<IEnumerable<PostDTO>> GetPostAsync()
        {
            var items = await _repository.GetEntitiesAsync();
            return _mapper.Map<List<Post>, List<PostDTO>>(items.ToList());
        }
    }
}
