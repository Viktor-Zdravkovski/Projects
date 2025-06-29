﻿namespace NotesApp.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get;set; }
        public string LastName { get;set; }
        public string Username { get;set; }
        public string Password { get;set; }
        public int Age { get; set; }

        public virtual List<Note> Notes { get; set; } = new List<Note>();
    }
}
