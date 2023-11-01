﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG.TestAssignment.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? BookGenre { get; set; }
        public int? AuthorId { get; set; }

        public Author? Author { get; set; }
    }
}