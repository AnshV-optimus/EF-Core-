using EntityFrameworkCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


    }
}
