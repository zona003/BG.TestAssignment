using BGNet.TestAssignment.Common.WebApi.Models;
using BGNet.TestAssignment.Models;

namespace BGNet.TestAssignment.Business.BusinessLogic.Interfaces
{
    public interface IAuthorService
    {
        public Task<ResponseWrapper<PagedResponce<List<AuthorDTO>>>> GetAuthors(int skip, int take, CancellationToken token);

        public Task<ResponseWrapper<AuthorDTO>> GetAuthor(int id, CancellationToken token);

        public Task<ResponseWrapper<AuthorDTO>> PutAuthor(int id, AuthorDTO authorDto, CancellationToken token);

        public Task<ResponseWrapper<AuthorDTO>> PostAuthor(AuthorDTO? authorDto, CancellationToken token);

        public Task<ResponseWrapper<AuthorDTO>> DeleteAuthor(int id, CancellationToken token);
    }
}
