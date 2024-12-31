using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Book>().HasData({
        //        new Book() {  Id = }
        //    }
                
        //        );
        //}
        public DbSet<Book> Books { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Author> Author { get; set; }

        public DbSet<BookPrice> BookPrices { get; set; }

        public DbSet<CurrencyType> CurrencyTypes { get; set; }


    }
}
