using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSystem.Data;
using BookSystem.Models;
using BookSystem.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase // Ensure the class is public
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET - api/books - Retrieves all books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthor()
        {
            return await _context.Authors.ToListAsync();
        }
        

      
      [HttpPost]
      public async Task<ActionResult<IEnumerable<Author>>> Post(Author author){

        if(author == null){
            return BadRequest();
        }
         
        // I want to found out if the author already exists
        if(ExistingAuthor(author.Name)){
            return Conflict("There is a book that already has this name");
        }
        
        //add author to context
         await _context.Authors.AddAsync(author);

         //save the changes
         await _context.SaveChangesAsync();

         return NoContent();

       
      }


        // Get - api/authors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(int id){
             
             //This will find the author
            var author = await _context.Authors.FindAsync(id);


            //If author is not found it should return not
            if(author == null){
                return NotFound();
            }

            return Ok(author);
            
        }



[HttpPut("id")]
public async Task<IActionResult> UpdateAuthor(int id, Author author){

  if (author == null)
    {
        return BadRequest("Author is null.");
    }  

if(id != author.Id){
    return Conflict("IDs DO NOT MATCH");
}

 var authorToUpdate = await _context.Authors.FindAsync(id);
 if(authorToUpdate == null){
    return NotFound();
 }
 
 authorToUpdate.Name = author.Name;
 authorToUpdate.Biography = author.Biography;

try{
    _context.Authors.Update(authorToUpdate);
await _context.SaveChangesAsync();
    
} 
catch (DbUpdateException ex)
    {
        // Log the error (uncomment ex variable name and write a log.)
        return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
    }


return NoContent();


}
       

 [HttpGet("authors-with-books")]
public async Task<ActionResult<List<AuthorBookDto>>> GetAuthorWithBooks()
{
    var joinResult = await _context.Authors.GroupJoin(
        _context.Books,
        author => author.Id,
        book => book.AuthorId,
        (author, books) => new AuthorBookDto
        {
            AuthorName = author.Name,
            BookTitles = books.Select(book => book.Title).ToList()
        }
    ).ToListAsync();

    return Ok(joinResult);
}



private bool ExistingAuthor(string authorName){
    
    return _context.Authors.Any(author => authorName == author.Name);
}


[HttpDelete("{id}")]
public async Task<IActionResult> RemoveAuthor(int id){

//Find author
var author = await _context.Authors.FindAsync(id);

if (author == null){
    return NotFound();
}

try{

_context.Authors.Remove(author);
_context.SaveChangesAsync();

} catch (DbUpdateException ex){
     return StatusCode(StatusCodes.Status500InternalServerError, "Failed removing");
}


return NoContent();
}


    }
    
   
}
