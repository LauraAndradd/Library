using Library.Entities;
using Library.Models;
using Library.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookInputModel request)
        {
            if (request == null)
                return BadRequest("Invalid book.");

            var book = new Book(request.Title, request.Author, request.ISBN, request.PublicationYear);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Ok("Book registered successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            var bookViewModels = books.Select(BookViewModel.FromEntity).ToList();
            return Ok(bookViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound("Book not found.");

            var bookViewModel = BookViewModel.FromEntity(book);
            return Ok(bookViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound("Book not found.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok("Book removed successfully!");
        }
    }
}
