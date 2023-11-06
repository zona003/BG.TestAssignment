using BGNet.TestAssignment.Common.WebApi.Models;
using BGNet.TestAssignment.Models;

namespace BGNet.TestAssignment.Business.BusinessLogic.Interfaces
{
    public interface IBooksService
    {
        public ResponseWrapper<List<BookDTO>> GetBooks();

        public Task<ResponseWrapper<BookDTO>> GetBook(int id);

        public ResponseWrapper<BookDTO> PutBook(int id, BookDTO bookDto);

        public ResponseWrapper<BookDTO> PostBook(BookDTO? bookDto);

        public Task<ResponseWrapper<BookDTO>> DeleteBook(int id);
    }
}
