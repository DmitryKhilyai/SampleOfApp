using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _service;

        public CommentController(IMapper mapper, ICommentService service)
        {
            _mapper = mapper;
            _service = service;
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<IEnumerable<CommentModel>> GetCommentsAsync()
        {
            var items = await _service.GetCommentsAsync();
            return _mapper.Map<List<CommentDTO>, List<CommentModel>>(items.ToList());
        }

        // GET: api/Comment/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(CommentModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCommentByIdAsync(int id)
        {
            var item = await _service.GetCommentAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<CommentDTO, CommentModel>(item);
            return Ok(model);
        }

        // POST: api/Comment
        [HttpPost]
        [ProducesResponseType(typeof(CommentModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CommentModel model)
        {
            try
            {
                var item = _mapper.Map<CommentModel, CommentDTO>(model);
                await _service.CreateCommentAsync(item);
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetCommentByIdAsync), new {id = model.Id}, model);
        }

        // PUT: api/Comment/5
        [HttpPut]
        [ProducesResponseType(typeof(CommentModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCommentAsync([FromBody] CommentModel model)
        {
            if (model.Id <= 0)
            {
                ModelState.AddModelError("comment.id", "The identifier must be a positive number.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var item = _mapper.Map<CommentModel, CommentDTO>(model);
                await _service.ChangeCommentAsync(item);

                return AcceptedAtAction(nameof(GetCommentByIdAsync), new { id = model.Id }, model);
            }
            catch (Exception e) //todo fix
            {
                return BadRequest();
            }
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CommentModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            try
            {
                await _service.DeleteCommentAsync(id);
                return Ok();
            }
            catch (ArgumentException e) //todo fix
            {
                return NotFound();
            }
        }
    }
}
