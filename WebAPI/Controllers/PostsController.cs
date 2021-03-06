﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;

        public PostsController(IPostService service)
        {
            _service = service;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<IEnumerable<PostDTO>> Get()
        {
            return await _service.GetPostAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "GetPost")]
        [ProducesResponseType(typeof(PostDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostByIdAsync(int id)
        {
            var model = await _service.GetPostAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // POST: api/Posts
        [HttpPost]
        [ProducesResponseType(typeof(PostDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePostAsync([FromBody] PostDTO model)
        {
            await _service.CreatePostAsync(model);
            return CreatedAtAction(nameof(GetPostByIdAsync), new {id = model.Id}, model);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PostDTO), StatusCodes.Status202Accepted)]
        public async Task<IActionResult> UpdatePostAsync(int id, [FromBody] PostDTO model)
        {
            await _service.ChangePostAsync(model);
            return AcceptedAtAction(nameof(GetPostByIdAsync), new {id = model.Id}, model);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePostAsync(int id)
        {
            await _service.DeletePostAsync(id);
            return Ok();
        }
    }
}
