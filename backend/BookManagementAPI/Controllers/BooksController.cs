using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return book;
        }

        // Modified POST method with validation
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book newBook)
        {
            // Validate the incoming book data
            if (newBook == null || string.IsNullOrEmpty(newBook.Title) || string.IsNullOrEmpty(newBook.Author) || string.IsNullOrEmpty(newBook.ISBN) || newBook.PublicationDate == default)
            {
                return BadRequest("Invalid book data. All fields are required.");
            }

            // Add the new book to the database
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            // Return the created book with its URI
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        // Modified PUT method to handle updating an existing book
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] Book updatedBook)
        {
            // Check if the ID matches
            if (id != updatedBook.Id) return BadRequest("Book ID mismatch.");

            // Check if the book exists
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null) return NotFound();

            // Update the book details
            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.ISBN = updatedBook.ISBN;
            existingBook.PublicationDate = updatedBook.PublicationDate;

            // Save changes to the database
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE method to remove a book by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
