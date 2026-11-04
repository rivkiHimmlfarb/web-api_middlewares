using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Interfaces;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookLibrary _library;

    public BooksController(IBookLibrary library)
    {
        _library = library;
    }

    // GET api/books
    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAll()
    {
        return Ok(_library.ReadAll());
    }

    // GET api/books/5
    [HttpGet("{id:int}")]
    public ActionResult<Book> GetById(int id)
    {
        var book = _library.Read(id);
        if (book is null)
        {
            return NotFound(new { message = $"Book with Id {id} not found." });
        }
        return Ok(book);
    }

    // POST api/books
    [HttpPost]
    public ActionResult<Book> Create([FromBody] Book book)
    {
        if (book is null)
        {
            return BadRequest(new { message = "Book body is required." });
        }

        _library.Create(book);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    // PUT api/books/5
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Book book)
    {
        if (book is null)
        {
            return BadRequest(new { message = "Book body is required." });
        }

        // המזהה בכתובת הוא הקובע, לא זה שבגוף הבקשה
        book.Id = id;

        try
        {
            _library.Update(book);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // DELETE api/books/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _library.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
