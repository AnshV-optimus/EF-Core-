using EntityFrameworkCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        

        private readonly AppDbContext context;
        public AuthorController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAuthor([FromBody] Author model)
        {
            context.Author.Add(model);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
