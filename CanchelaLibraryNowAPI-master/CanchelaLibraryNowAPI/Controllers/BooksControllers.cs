using DoradoLibraryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CanchelaLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksControllers : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
           new Book { Id = 1, Title = "Mein Kampf", Author = "Adolf Hitler", Genre = "Autobiography", Available = true, PublishedYear = 1925 },
           new Book { Id = 2, Title = "The Parallel Lives", Author = "Loeb", Genre = "Autobiography", Available = true, PublishedYear = 1919 }
        };
        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(new { status = "success", data = books, Message = "Books retrieved." });
        }
        [HttpGet("{id}")]
        public IActionResult getById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Books not found" });
            return Ok(new { status = "success", data = book, message = "Books retrieved" });
        }
        [HttpPost("{id}")]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(getById), new { id = newBook.Id },
                new { status = "success", data = newBook, message = "Books created" });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Books not found" });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new { status = "success", data = book, Message = "Boks updated." });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { 
            var book = books.FirstOrDefault((book) => book.Id == id);
            if (book == null)
            {
                return NotFound(new { status = "error", message = "Book not found" });
            }
            books.Remove(book);
            return Ok(new { status = "success", message = "Book deleted" });
        }
    }
}
