﻿using BG.TestAssignment.DataAccessLayer.DataContext;
using BG.TestAssignment.Models;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.TestAssignment.Business.BusinessLogic
{
    public class BooksBL
    {
        private BookAuthorsDataContext Context { get; set; }

        public BooksBL(BookAuthorsDataContext dbContext)
        {
            Context = dbContext;
        }

        public List<BookDTO> GetBooks()
        {
            return Context.Books.AsQueryable().Adapt<List<BookDTO>>();
        }

        public BookDTO GetBook(int id)
        {
            var result = Context.Books.FirstOrDefault(a => a.Id == id);
            if (result == null) return new BookDTO();

            return result.Adapt<BookDTO>();
        }

        public bool PutBook(int id, BookDTO bookDto)
        {
            if (id != bookDto.Id)
            {
                return false;
            }

            var book = bookDto.Adapt<Book>();

            Context.Entry(book).State = EntityState.Modified;

            try
            {
                Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public bool PostBook(BookDTO? bookDto)
        {
            if (bookDto == null)
            {
                return false;
            }

            var book = bookDto.Adapt<Book>();

            Context.Books.Add(book);
            Context.SaveChangesAsync();

            return true;
        }

        public bool DeleteBook(int id)
        {

            if (Context.Books == null)
            {
                return false;
            }
            var book = Context.Books.FindAsync(id).Result;
            if (book == null)
            {
                return false;
            }

            Context.Books.Remove(book);
            Context.SaveChangesAsync();

            return true;
        }

        private bool BookExists(int id)
        {
            return (Context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
