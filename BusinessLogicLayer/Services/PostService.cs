using System;
using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IRepository1<Post> _repository;

        public PostService(IMapper mapper, IRepository1<Post> repository)
        {
            _mapper = mapper;
            _repository = repository;
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

        public async Task CreatePostAsync(PostDTO dTO)
        {
            var item = _mapper.Map<PostDTO, Post>(dTO);
            _repository.Create(item);
            await _repository.SaveAsync();
        }

        public async Task ChangePostAsync(PostDTO dTO)
        {
            var item = _mapper.Map<PostDTO, Post>(dTO);
            _repository.Update(item);
            await _repository.SaveAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            try
            {
                _repository.Delete(id);
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ArgumentException($"The post entity with {id} identifier does not exist in the database.", e);
            }
        }
    }
}
