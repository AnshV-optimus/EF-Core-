﻿namespace EntityFrameworkCoreApp.Data
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? NoOfPages  { get; set; }

        public bool IsActive { get; set; }

        public int? AuthorId { get; set; }

        public virtual Author? Author { get; set; }

        public int? LanguageId { get; set; }

        public virtual Language? Language { get; set; }  

    }
}
