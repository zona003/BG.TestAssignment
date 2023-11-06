using System.Reflection.Metadata.Ecma335;
using BG.TestAssignment.DataAccess;
using BGNet.TestAssignment.Business.BusinessLogic.Interfaces;
using BGNet.TestAssignment.Common.WebApi.Models;
using BGNet.TestAssignment.DataAccess;
using BGNet.TestAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGNet.TestAssignment.Api.Library.Controllers
{
    [Authorize]
    [Route("api/lib/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBooksService BooksService { get; set; }

        public BooksController(BookAuthorsDataContext context, IBooksService booksService)
        {
            BooksService = booksService;
        }

        [HttpGet("")]
        public async Task<ActionResult<ResponseWrapper<List<AuthorDTO>>>> GetBooks()
        {
            return Ok(BooksService.GetBooks());
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWrapper<BookDTO>>> GetBook(int id)
        {
            return await BooksService.GetBook(id);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult<ResponseWrapper<BookDTO>> PutBook(int id, BookDTO authorDto)
        {
            return BooksService.PutBook(id, authorDto);
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponseWrapper<BookDTO>>> PostBook(BookDTO authorDto)
        {
            return BooksService.PostBook(authorDto);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper<BookDTO>>> DeleteBook(int id)
        {
            return await BooksService.DeleteBook(id);
        }
    }
}
