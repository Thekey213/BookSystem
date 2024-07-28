using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSystem.Data;
using BookSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase // Ensure the class is public
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET - api/books - Retrieves all books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }



         // GET - api/books/{id} - Retrieves a specific book by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id){
             
               
            var book = await _context.Books.FindAsync(id);

            if(book == null){
                return NotFound();
            }

            return Ok(book);
            

                
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book){



       if(book == null){
        throw new ArgumentNullException(nameof(book), "This can not be null");
       }

       

            //Add book to context
            _context.Books.Add(book);
             
             //Saves contexts to database 
            await _context.SaveChangesAsync();

            return Ok(book);
            
        }
        
        
 [HttpPut("{id}")]
public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
{
    if (id != updatedBook.Id)
    {
        return BadRequest("Book ID mismatch.");
    }

    var book = await _context.Books.FindAsync(id);
    if (book == null)
    {
        return NotFound();
    }

    book.Title = updatedBook.Title;
    book.AuthorId = updatedBook.AuthorId;
    book.PublishedYear = updatedBook.PublishedYear;

    _context.Entry(book).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!BookExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return NoContent();
}


  private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }



[HttpDelete("{id}")]
public async Task<IActionResult> DeleteBook(int id){


// find the book first
 var theBook = await _context.Books.FindAsync(id);

 if(theBook == null){
    return NotFound(); 
 }  

//Removed the book from the context
_context.Books.Remove(theBook);

//saves the the updates of context
await _context.SaveChangesAsync();

//returns Indicates that the request was successful but there is no content to return. (204)
return NoContent();

    
}
  


    }

 
}
