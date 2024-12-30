using EntityFrameworkCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreApp.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult GetAllCurrencies()
        {
            var result = _appDbContext.CurrencyTypes.ToList();
            return Ok(result);
        }

        [HttpGet("{currency}")]
        public async Task<IActionResult> GetAllRecords([FromRoute] string currency, [FromQuery] string? Description)
        {

            var result = await _appDbContext.CurrencyTypes
                            .FirstOrDefaultAsync(c => c.Currency == currency && (string.IsNullOrEmpty(Description) || c.Description == Description));
            return Ok(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetCurrenciesByIds([FromBody] List<int>ids)
        {
            var result = await _appDbContext.CurrencyTypes
                            .Where(c => ids.Contains(c.Id))
                            .ToListAsync();

            return Ok(result);
        }

        [HttpGet("custom")]
        public async Task<IActionResult> GetCustomCurrencies()
        {
            var result = await _appDbContext.CurrencyTypes
                            .Select( c => new
                            {
                                CurrencyId = c.Id,
                                CurrencyName = c.Currency
                            })
                            .ToListAsync();

            return Ok(result);
        }
    }
}
