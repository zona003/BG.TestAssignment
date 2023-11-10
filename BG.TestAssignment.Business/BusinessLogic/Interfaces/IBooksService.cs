﻿using BGNet.TestAssignment.Common.WebApi.Models;
using BGNet.TestAssignment.Models;

namespace BGNet.TestAssignment.Business.BusinessLogic.Interfaces
{
    public interface IBooksService
    {
        public Task<ResponseWrapper<PagedResponce<List<BookDto>>>> GetBooks(int? skip, int? take, CancellationToken token);

        public Task<ResponseWrapper<BookDto>> GetBook(int id, CancellationToken token);

        public Task<ResponseWrapper<BookDto>> PutBook(int id, BookDto bookDto, CancellationToken token);

        public Task<ResponseWrapper<AddBookRequest>> PostBook(AddBookRequest bookDto, CancellationToken token);

        public Task<ResponseWrapper<BookDto>> DeleteBook(int id, CancellationToken token);
    }
}