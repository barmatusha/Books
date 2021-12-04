using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IEnumerable<Book> _books;
        private readonly IEnumerable<Author> _authors;
        private readonly IMapper _mapper;
        public BooksController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _books = _repository.Book.GetBooks();
            _authors = _repository.Author.GetAuthors();
            _mapper = mapper;
        }
        [HttpGet("{id}", Name = "BookById")]
        public IActionResult GetBook(int id)
        {
            var book = _repository.Book.GetBook(id, trackChanges: true);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                List<string> authros = new List<string>();
                foreach (var obj in book.Authors)
                {
                    authros.Add(obj.Surname);
                }
                var bookDto = _mapper.Map<BookDto>(book);
                bookDto.AuthorsList = authros;
                return Ok(bookDto);
            }
        }

        [HttpGet("AuthorBooks")]
        public IActionResult GetBooksForAuthor(int id)
        {
            var author = _repository.Author.GetAuthor(id, trackChanges: false);
            if (author == null)
            {
                return NotFound();
            }
            var books = _authors.First(a => a.AuthorId == id).Books;
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(booksDto);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] BookForCreationDto book)
        {
            if (book == null)
                return BadRequest("AuthorForCreationDto obj is null");
            var bookEntity = _mapper.Map<Book>(book);
            var author = _repository.Author.GetAuthor(book.AuthorId, trackChanges: true);
            if (author == null)
            {
                return NotFound();
            }
            bookEntity.Authors.Add(author);
            _repository.Book.CreateBook(bookEntity);
            foreach (var x in bookEntity.Authors) { Console.WriteLine(x); }
            _repository.Save();
            var bookToReturn = _mapper.Map<BookDto>(bookEntity);
            return CreatedAtRoute("BookById", new { id = bookToReturn.Id }, bookToReturn);

        }

        [HttpPut("AddBookForAuthor")]
        public IActionResult AddBookForAuthor([FromBody] BookForAuthorAddDto bookForAuthorAdd)
        {
            if (bookForAuthorAdd == null)
                return BadRequest("bookForAuthorAddDto obj is null");
            var author = _repository.Author.GetAuthor(bookForAuthorAdd.AuthorId, trackChanges: true);
            var book = _repository.Book.GetBook(bookForAuthorAdd.BookId, trackChanges: true);
            author.Books.Add(book);
            _repository.Author.UpdateAuthor(author);
            _repository.Save();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _repository.Book.GetBook(id, trackChanges: true);
            if (book == null)
            {
                return NotFound();
            }
            _repository.Book.DeleteBook(book);
            _repository.Save();
            return NoContent();
        }

    }
}
