using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService service)
        {
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
        [ValidateModel]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CommentDTO model)
        {
            await _service.CreateCommentAsync(model);
            return CreatedAtAction(nameof(GetCommentByIdAsync), new {id = model.Id}, model);
        }

        // PUT: api/Comments/5
        [HttpPut]
        [ProducesResponseType(typeof(CommentDTO), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateModel]
        public async Task<IActionResult> UpdateCommentAsync([FromBody] CommentDTO model)
        {
            await _service.ChangeCommentAsync(model);
            return AcceptedAtAction(nameof(GetCommentByIdAsync), new { id = model.Id }, model);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            await _service.DeleteCommentAsync(id);
            return Ok();
        }
    }
}
