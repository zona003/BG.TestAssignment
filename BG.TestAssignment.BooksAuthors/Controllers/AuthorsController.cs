using BG.TestAssignment.Business.BusinessLogic;
using BG.TestAssignment.Business.BusinessLogic.Interfaces;
using BG.TestAssignment.DataAccessLayer.DataContext;
using BG.TestAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.BooksAuthors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorBL AuthorsBl { get; set; }


        public AuthorsController(BookAuthorsDataContext context, IAuthorBL authorBl)
        {
            AuthorsBl = authorBl;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthors()
        {
            return Ok(AuthorsBl.GetAuthors());
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public  ActionResult<AuthorDTO> GetAuthor(int id)
        {
            return Ok(AuthorsBl.GetAuthor(id));
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDTO authorDto)
        {
            bool result = AuthorsBl.PutAuthor(id, authorDto);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDTO authorDto)
        {
            bool result = AuthorsBl.PostAuthor(authorDto);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {

            bool result = AuthorsBl.DeleteAuthor(id);

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
