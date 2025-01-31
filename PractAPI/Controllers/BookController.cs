using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace PractAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static readonly List<Book> books = [
            new Book(1, "Book 1", "Author 1"),
            new Book(2, "Book 2", "Author 2"),
            new Book(3, "Book 3", "Author 3"),
            new Book(4, "Book 4", "Author 4"),
            new Book(5, "Book 5", "Author 5"),
            new Book(6, "Book 6", "Author 6"),
            new Book(7, "Book 7", "Author 7"),
            new Book(8, "Book 8", "Author 8"),
            new Book(9, "Book 9", "Author 9"),
            new Book(10, "Book 10", "Author 10"),
            ];

        private static int id = 11;

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            Book? book = GetBookById(id);

            if (book != null)
            {
                return Ok(book);
            }

            return NotFound("Book with this id doesn't exist");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Book> AddBook([FromBody] Book book)
        {
            books.Add(book);

            book.Id = id;
            id++;

            return StatusCode(201, book);
        }

        [HttpPatch("{id}")]
        [Authorize]
        public ActionResult<Book> UpdateBook(int id, [FromBody] Book book)
        {
            Book? oldBook = GetBookById(id);

            if (oldBook != null)
            {
                oldBook.Name = book.Name;
                oldBook.Author = book.Author;

                return Ok(oldBook);
            }

            return NotFound("Book with this id doesn't exist");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int id)
        {
            Book? book = GetBookById(id);

            if (book != null)
            {
                books.Remove(book);

                return NoContent();
            }

            return NotFound("Book with this id doesn't exist");
        }

        public Book? GetBookById(int id)
        {
            return books.FirstOrDefault(book => book.Id == id);
        } 
    }
}
