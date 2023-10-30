﻿using BG.TestAssignment.Business.BusinessLogic;
using BG.TestAssignment.DataAccessLayer.DataContext;
using BG.TestAssignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BG.TestAssignment.BooksBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksBL BooksBl { get; set; }

        public BooksController(BookAuthorsDataContext context)
        {
            BooksBl = new BooksBL(context);
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            return Ok(BooksBl.GetBooks());
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            return Ok(BooksBl.GetBook(id));
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDTO authorDto)
        {
            bool result = BooksBl.PutBook(id, authorDto);

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
        public async Task<ActionResult<Book>> PostBook(BookDTO authorDto)
        {
            bool result = BooksBl.PostBook(authorDto);

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

            bool result = BooksBl.DeleteBook(id);

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
