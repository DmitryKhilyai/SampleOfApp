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
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _service;

        public PostsController(IMapper mapper, IPostService service)
        {
            _mapper = mapper;
            _service = service;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<IEnumerable<PostModel>> Get()
        {
            var items = await _service.GetPostAsync();
            return _mapper.Map<List<PostDTO>, List<PostModel>>(items.ToList());
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "GetPost")]
        [ProducesResponseType(typeof(PostModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostByIdAsync(int id)
        {
            var item = await _service.GetPostAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<PostDTO, PostModel>(item);
            return Ok(model);
        }

        // POST: api/Posts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
