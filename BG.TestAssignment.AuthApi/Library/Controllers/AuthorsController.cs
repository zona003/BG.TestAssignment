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
        public ActionResult<ResponseWrapper<List<AuthorDTO>>> GetAuthors( int page = 1)
        {
            return AuthorsService.GetAuthors(page);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public ActionResult<ResponseWrapper<AuthorDTO>> GetAuthor(int id)
        {
            return  AuthorsService.GetAuthor(id).Result;
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
        public ActionResult<ResponseWrapper<AuthorDTO>> PostAuthor(AuthorDTO authorDto)
        {
            return  AuthorsService.PostAuthor(authorDto);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public ActionResult<ResponseWrapper<AuthorDTO>> DeleteAuthor(int id)
        {
            return   AuthorsService.DeleteAuthor(id).Result;
        }

    }
}
