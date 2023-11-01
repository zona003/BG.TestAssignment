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
    public interface IAuthorBL
    {
        public List<AuthorDTO> GetAuthors();

        public AuthorDTO GetAuthor(int id);

        public bool PutAuthor(int id, AuthorDTO authorDto);

        public bool PostAuthor(AuthorDTO? authorDto);

        public bool DeleteAuthor(int id);
    }
}
