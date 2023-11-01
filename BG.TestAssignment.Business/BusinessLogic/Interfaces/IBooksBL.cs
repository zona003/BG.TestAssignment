using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.TestAssignment.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.Business.BusinessLogic.Interfaces
{
    public interface IBooksBL
    {
        public List<BookDTO> GetBooks();

        public BookDTO GetBook(int id);

        public bool PutBook(int id, BookDTO bookDto);

        public bool PostBook(BookDTO? bookDto);

        public bool DeleteBook(int id);
    }
}
