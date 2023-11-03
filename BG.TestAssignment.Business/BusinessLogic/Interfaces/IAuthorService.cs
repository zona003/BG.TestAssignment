using BGNet.TestAssignment.Models;

namespace BGNet.TestAssignment.Business.BusinessLogic.Interfaces
{
    public interface IAuthorService
    {
        public List<AuthorDTO> GetAuthors();

        public AuthorDTO? GetAuthor(int id);

        public bool PutAuthor(int id, AuthorDTO authorDto);

        public bool PostAuthor(AuthorDTO? authorDto);

        public bool DeleteAuthor(int id);
    }
}
