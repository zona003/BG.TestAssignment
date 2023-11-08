using BGNet.TestAssignment.Common.WebApi.Models;
using BGNet.TestAssignment.Models;

namespace BGNet.TestAssignment.Business.BusinessLogic.Interfaces
{
    public interface IBooksService
    {
        public Task<ResponseWrapper<PagedResponce<List<BookDTO>>>> GetBooks(int skip, int take, CancellationToken token);

        public Task<ResponseWrapper<BookDTO>> GetBook(int id, CancellationToken token);

        public Task<ResponseWrapper<BookDTO>> PutBook(int id, BookDTO bookDto, CancellationToken token);

        public Task<ResponseWrapper<BookDTO>> PostBook(BookDTO? bookDto, CancellationToken token);

        public Task<ResponseWrapper<BookDTO>> DeleteBook(int id, CancellationToken token);
    }
}