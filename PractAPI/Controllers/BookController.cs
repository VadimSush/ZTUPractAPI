using Microsoft.AspNetCore.Mvc;

namespace PractAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly List<Book> books = [
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

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            Book? book = books.FirstOrDefault(x => x.Id == id);

            if (book != null)
            {
                return Ok(book);
            }

            return NotFound();
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
