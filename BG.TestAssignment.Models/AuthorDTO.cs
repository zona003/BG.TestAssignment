﻿namespace BGNet.TestAssignment.Models
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }

        //public List<BookDto>? Books { get; set; }
    }
}
