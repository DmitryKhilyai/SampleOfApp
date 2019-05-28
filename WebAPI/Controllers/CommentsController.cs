using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _service;

        public CommentsController(IMapper mapper, ICommentService service)
        {
            _mapper = mapper;
            _service = service;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IEnumerable<CommentDTO>> GetCommentsAsync()
        {
            return await _service.GetCommentsAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}", Name = "GetComment")]
        [ProducesResponseType(typeof(CommentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCommentByIdAsync(int id)
        {
            var model = await _service.GetCommentAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // POST: api/Comments
        [HttpPost]
        [ProducesResponseType(typeof(CommentDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CommentDTO model)
        {
            try
            {
                await _service.CreateCommentAsync(model);
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetCommentByIdAsync), new {id = model.Id}, model);
        }

        // PUT: api/Comments/5
        [HttpPut]
        [ProducesResponseType(typeof(CommentDTO), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCommentAsync([FromBody] CommentDTO model)
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
                await _service.ChangeCommentAsync(model);

                return AcceptedAtAction(nameof(GetCommentByIdAsync), new { id = model.Id }, model);
            }
            catch (Exception e) //todo fix
            {
                return BadRequest();
            }
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CommentDTO), StatusCodes.Status200OK)]
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
