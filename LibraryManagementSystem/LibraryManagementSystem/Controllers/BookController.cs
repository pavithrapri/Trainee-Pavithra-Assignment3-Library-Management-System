using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class BookController : ControllerBase
    {
        private readonly ICosmosDbService<Book> _cosmosDbService;

        public BookController(ICosmosDbService<Book> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(string id)
        {
            var book = await _cosmosDbService.GetItemAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public async Task<ActionResult> AddBook(Book book)
        {
            await _cosmosDbService.AddItemAsync(book, book.UId);
            return CreatedAtAction(nameof(GetBookById), new { id = book.UId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(string id, Book book)
        {
            if (id != book.UId)
            {
                return BadRequest();
            }

            await _cosmosDbService.UpdateItemAsync(id, book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
