using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IList<Author> _authors;
        private readonly IMapper _mapper;
        public AuthorsController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _authors = _repository.Author.GetAuthors();
            _mapper = mapper;
        }
        [HttpGet(Name = "GetAllAuthors")]
        public IActionResult GetAuthors()
        {
            var authors = _repository.Author.GetAuthors();
            var authorDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);

            return Ok(authorDto);
        }
        
        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorForCreationDto author)
        {
            if (author == null)
                return BadRequest("AuthorForCreationDto obj is null");
            var authorEntity = _mapper.Map<Author>(author);
            _repository.Author.CreateAuthor(authorEntity);
            _repository.Save();
            return CreatedAtRoute("GetAllAuthors", new { authorId = authorEntity.AuthorId }, authorEntity);
        }

        

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _repository.Author.GetAuthor(id, trackChanges: true);
            if (author == null)
            {
                return NotFound();
            }
            _repository.Author.DeleteAuthor(author);
            _repository.Save();
            return NoContent();
        }
    }

}
