using EntityFrameworkCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EntityFrameworkCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public BookController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("")]

        public async Task<IActionResult> AddNewBook([FromBody] Book model)
        {


            //MANUAL WAY OF ADDING AUTHOR WHICH IS RELATED TO THE BOOKS TABLE 

            //var author = new Author()
            //{
            //    Name = "Ansh",
            //    Email = "Vermaansh708@gmail.com"
            //};
            //model.Author = author; 

            _appDbContext.Books.Add(model);
            await _appDbContext.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooks([FromBody] List<Book> model)
        {
            _appDbContext.Books.AddRange(model);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{BookId}")]

        public async Task<IActionResult> UpdateBook([FromRoute] int BookId, [FromBody] Book model)
        {
            var result = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == BookId);

            if (result == null)
            {
                return NotFound();
            }

            result.Title = model.Title;
            result.Description = model.Description;

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("AnotherUpdate")]
        public async Task<IActionResult> AnotherUpdateBookMethod([FromBody] Book model)
        {

            _appDbContext.Books.Update(model);

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("BulkUpdate")]

        public async Task<IActionResult> BulkUpdateInBooks([FromQuery] int Pages)
        {
            var result = await _appDbContext.Books.ExecuteUpdateAsync(
                            x => x.SetProperty(x => x.Title, "Title Updated"));

            var value = await _appDbContext.Books
                            .Where(x => x.NoOfPages == Pages)
                            .ExecuteUpdateAsync(x => x
                            .SetProperty(x => x.IsActive, true)
                            .SetProperty(x => x.Title, x => x.Title + "2.0"));


            return Ok(value);
        }

        [HttpDelete("{BookId}")]
        public async Task<IActionResult> DeleteBookById([FromRoute] int BookId)
        {
            var result = _appDbContext.Books.Find(BookId);

            if (result == null)
                return NotFound();

            _appDbContext.Books.Remove(result);

            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("DeleteBulk")]

        public async Task<IActionResult> DeleteBooksInBulk()
        {
            //var result = _appDbContext.Books.Where(x=>x.Id <= 2).ToList();

            //_appDbContext.Books.RemoveRange(result);

            //await _appDbContext.SaveChangesAsync();

            //METHOD 2

            var result = await _appDbContext.Books
                            .Where(x => x.Id <= 3)
                            .ExecuteDeleteAsync();


            return Ok();
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooksJoins()
        {
            var result = await _appDbContext.Books
                            .Select(x => new
                            {
                                x.Id,
                                x.Title,
                                LangTitle = x.Language.Title,
                                x.Author.Email
                            }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("Eager")]

        public async Task<IActionResult> GetBooksByEargerLoading()
        {
            var result = await _appDbContext.Author
                        .Include(b => b.Books).ToListAsync();

            return Ok(result);
        }

        [HttpGet("BulkLazy")]
        public async Task<IActionResult> GetBooksByLazyLoading()
        {
            try
            {
                var result = await _appDbContext.Books.ToListAsync();

                var BooksWithAutor = new List<object>();

                foreach(var book in result)
                {
                    var author = book.Author;

                    BooksWithAutor.Add(new
                    {
                        BoolTitle = book.Title,
                        AuthorName = author?.Name

                    });

                }

                return Ok(BooksWithAutor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }



}
