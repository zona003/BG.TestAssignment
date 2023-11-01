using BG.TestAssignment.Business.BusinessLogic;
using BG.TestAssignment.Business.BusinessLogic.Interfaces;
using BG.TestAssignment.DataAccess;
using BG.TestAssignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BG.TestAssignment.BooksBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBooksService BooksService { get; set; }

        public BooksController(BookAuthorsDataContext context, IBooksService booksService)
        {
            BooksService = booksService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            return Ok(BooksService.GetBooks());
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            return Ok(BooksService.GetBook(id));
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDTO authorDto)
        {
            bool result = BooksService.PutBook(id, authorDto);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBook(BookDTO authorDto)
        {
            bool result = BooksService.PostBook(authorDto);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {

            bool result = BooksService.DeleteBook(id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
