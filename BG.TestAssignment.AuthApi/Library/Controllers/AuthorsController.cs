using BG.TestAssignment.DataAccess;
using BGNet.TestAssignment.Business.BusinessLogic.Interfaces;
using BGNet.TestAssignment.DataAccess;
using BGNet.TestAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGNet.TestAssignment.Api.Library.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorService AuthorsService { get; set; }


        public AuthorsController(BookAuthorsDataContext context, IAuthorService authorService)
        {
            AuthorsService = authorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthors()
        {
            return Ok(AuthorsService.GetAuthors());
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public  ActionResult<AuthorDTO> GetAuthor(int id)
        {
            var result = AuthorsService.GetAuthor(id);
            if (result == null)
            {
                return BadRequest("Not exist");
            }
            return Ok(result);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDTO authorDto)
        {
            bool result = AuthorsService.PutAuthor(id, authorDto);

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
        public async Task<IActionResult> PostAuthor(AuthorDTO authorDto)
        {
            bool result = AuthorsService.PostAuthor(authorDto);

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

            bool result = AuthorsService.DeleteAuthor(id);

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
