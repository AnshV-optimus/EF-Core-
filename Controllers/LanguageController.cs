using EntityFrameworkCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreApp.Controllers
{
    [Route("api/Languages")]
    [ApiController]
    public class LanguageController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public LanguageController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLanguages()
        {
            var result = await _appDbContext.Languages.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}:int")]
        public async Task<IActionResult> GetLanguageById([FromRoute] int id)
        {

            var result = await _appDbContext.Languages.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("{Name}")]

        public async Task<IActionResult> GetLanguageByName([FromRoute] string Name)
        {
            var result = await _appDbContext.Languages
                            .Where(l => l.Title == Name)
                            .FirstAsync();

            //var res = await _appDbContext.Languages
            //            .SingleOrDefaultAsync(l => l.Title == Name);        

            return Ok(result);
        }

        
    }
}
