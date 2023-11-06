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
    public class AuthorsController : ControllerBase
    {
        private IAuthorService AuthorsService { get; set; }


        public AuthorsController(IAuthorService authorService)
        {
            AuthorsService = authorService;
        }

        [HttpGet]
        public ActionResult<ResponseWrapper<List<AuthorDTO>>> GetAuthors()
        {
            return Ok(AuthorsService.GetAuthors());
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async  Task<ActionResult<ResponseWrapper<AuthorDTO>>> GetAuthor(int id)
        {
            return await AuthorsService.GetAuthor(id);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public  ActionResult<ResponseWrapper<AuthorDTO>> PutAuthor(int id, AuthorDTO authorDto)
        {
            return  AuthorsService.PutAuthor(id, authorDto);
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponseWrapper<AuthorDTO>>> PostAuthor(AuthorDTO authorDto)
        {
            return  AuthorsService.PostAuthor(authorDto);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper<AuthorDTO>>> DeleteAuthor(int id)
        {
            return  await AuthorsService.DeleteAuthor(id);
        }

    }
}
