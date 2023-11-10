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
        public async Task<ActionResult<ResponseWrapper<PagedResponce<List<BookDto>>>>> GetBooks(int? skip, int? take, CancellationToken token = default)
        {
            return await BooksService.GetBooks(skip, take, token);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWrapper<BookDto>>> GetBook(int id, CancellationToken token)
        {
            return await BooksService.GetBook(id, token);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseWrapper<BookDto>>> PutBook(int id, BookDto authorDto, CancellationToken token)
        {
            return await BooksService.PutBook(id, authorDto, token);
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponseWrapper<AddBookRequest>>> PostBook(AddBookRequest authorDto, CancellationToken token)
        {
            return await BooksService.PostBook(authorDto, token);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper<BookDto>>> DeleteBook(int id, CancellationToken token)
        {
            return await BooksService.DeleteBook(id, token);
        }
    }
}