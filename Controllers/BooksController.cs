using Microsoft.AspNetCore.Mvc;
using BookLendingSystem.Data;
using BookLendingSystem.Models;

namespace BookLendingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(_context.Books.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            book.AvailableCopies = book.TotalCopies;
            book.IsAvailable = book.TotalCopies > 0;
            // ISBN is now included in the Book model and will be saved automatically
            _context.Books.Add(book);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.Year = updatedBook.Year;
            book.Description = updatedBook.Description;
            book.TotalCopies = updatedBook.TotalCopies;
            book.AvailableCopies = updatedBook.AvailableCopies;
            book.IsAvailable = book.AvailableCopies > 0;
            book.ISBN = updatedBook.ISBN;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
