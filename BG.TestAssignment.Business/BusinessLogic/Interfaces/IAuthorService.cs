using BGNet.TestAssignment.Common.WebApi.Models;
using BGNet.TestAssignment.Models;

namespace BGNet.TestAssignment.Business.BusinessLogic.Interfaces
{
    public interface IAuthorService
    {
        public Task<ResponseWrapper<PagedResponce<List<AuthorDto>>>> GetAuthors(int? skip, int? take, CancellationToken token);

        public Task<ResponseWrapper<AuthorDto>> GetAuthor(int id, CancellationToken token);

        public Task<ResponseWrapper<AuthorDto>> PutAuthor(int id, AuthorDto authorDto, CancellationToken token);

        public Task<ResponseWrapper<AuthorDto>> PostAuthor(AuthorDto? authorDto, CancellationToken token);

        public Task<ResponseWrapper<AuthorDto>> DeleteAuthor(int id, CancellationToken token);
    }
}