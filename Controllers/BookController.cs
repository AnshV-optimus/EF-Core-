using EntityFrameworkCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
