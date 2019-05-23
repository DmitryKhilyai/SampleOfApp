using System;
using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Comment> _repository;

        public CommentService(IMapper mapper, IRepository<Comment> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task CreateCommentAsync(CommentDTO itemDto)
        {
            var item = _mapper.Map<CommentDTO, Comment>(itemDto);
            await _repository.CreateAsync(item);
        }

        public async Task ChangeCommentAsync(CommentDTO itemDto)
        {
            var item = _mapper.Map<CommentDTO, Comment>(itemDto);
            await _repository.UpdateAsync(item);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<CommentDTO> GetCommentAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var item = await _repository.GetByIdAsync(id.Value);
            return _mapper.Map<Comment, CommentDTO>(item);
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsAsync()
        {
            var items = await _repository.GetEntitiesAsync();
            return _mapper.Map<List<Comment>, List<CommentDTO>>(items.ToList());
        }
    }
}
