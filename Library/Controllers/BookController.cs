using Microsoft.AspNetCore.Mvc;
using Applications.Dto.Book;

using Domain.Models;
using Services.Interfaces;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/bookController/")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddBookAsync([FromBody] AddBookDto addBookDto, CancellationToken token)
        {
            var libraryCode = await _bookService.AddBookAsync(addBookDto, token);
            return Ok(libraryCode);
        }
        
        [HttpGet("{libraryCode}")]
        public async Task<IActionResult> GetBookByCodeAsync(string libraryCode, CancellationToken token)
        {
            var book = await _bookService.GetBookByCodeAsync(libraryCode, token);
            return Ok(book);
        }
        
        [HttpGet("byTitle/{title}")]
        public async Task<IActionResult> GetBookByNameAsync(string title, CancellationToken token)
        {
            var book = await _bookService.GetBookByNameAsync(title, token);
            return Ok(book);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetBooks(CancellationToken token)
        {
            var books = await _bookService.GetBooksAsync(token);
            return Ok(books);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto updateBookDto, CancellationToken token)
        {
            var result = await _bookService.UpdateBookAsync(updateBookDto, token);
            if (!result)
                return NotFound("Книга не найдена для обновления");

            return Ok("Книга успешно обновлена");
        }

        [HttpDelete("{libraryCode}")]
        public async Task<IActionResult> RemoveBook(string libraryCode, CancellationToken token)
        {
            var result = await _bookService.RemoveBookAsync(libraryCode, token);
            if (!result)
                return NotFound("Книга не найдена для удаления" );

            return Ok("Книга успешно удалена");
        }
    }
}
