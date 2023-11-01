using BG.TestAssignment.Models;

namespace BG.TestAssignment.Business.BusinessLogic.Interfaces
{
    public interface IBooksService
    {
        public List<BookDTO> GetBooks();

        public BookDTO GetBook(int id);

        public bool PutBook(int id, BookDTO bookDto);

        public bool PostBook(BookDTO? bookDto);

        public bool DeleteBook(int id);
    }
}
