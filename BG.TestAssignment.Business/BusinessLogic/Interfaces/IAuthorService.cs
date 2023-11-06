using BGNet.TestAssignment.Common.WebApi.Models;
using BGNet.TestAssignment.Models;

namespace BGNet.TestAssignment.Business.BusinessLogic.Interfaces
{
    public interface IAuthorService
    {
        public ResponseWrapper<List<AuthorDTO>> GetAuthors();

        public Task<ResponseWrapper<AuthorDTO>> GetAuthor(int id);

        public ResponseWrapper<AuthorDTO> PutAuthor(int id, AuthorDTO authorDto);

        public ResponseWrapper<AuthorDTO> PostAuthor(AuthorDTO? authorDto);

        public Task<ResponseWrapper<AuthorDTO>> DeleteAuthor(int id);
    }
}
