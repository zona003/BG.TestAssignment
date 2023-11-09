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
        public async Task<ActionResult<ResponseWrapper<PagedResponce<List<AuthorDto>>>>> GetAuthors(CancellationToken token, int? skip, int? take)
        {
            return await AuthorsService.GetAuthors(skip, take, token);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWrapper<AuthorDto>>> GetAuthor(int id, CancellationToken token)
        {
            return await AuthorsService.GetAuthor(id, token);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseWrapper<AuthorDto>>> PutAuthor(int id, AuthorDto authorDto, CancellationToken token)
        {
            return await AuthorsService.PutAuthor(id, authorDto, token);
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponseWrapper<AuthorDto>>> PostAuthor(AuthorDto authorDto, CancellationToken token)
        {
            return await AuthorsService.PostAuthor(authorDto, token);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper<AuthorDto>>> DeleteAuthor(int id, CancellationToken token)
        {
            return await AuthorsService.DeleteAuthor(id, token);
        }

    }
}