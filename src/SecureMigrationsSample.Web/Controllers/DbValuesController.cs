using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SecureMigrationsSample.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbValuesController : ControllerBase
    {
        private readonly SampleContext _ctx;
        public DbValuesController(SampleContext ctx) => _ctx = ctx;

        [HttpGet]
        public async Task<ActionResult<ICollection<Secret>>> Get()
            => await _ctx.Chamber.ToListAsync();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Secret>> GetById(int id)
        {
            var secret = await _ctx.Chamber.FirstOrDefaultAsync(s => s.Id == id);
            return secret != null ? Ok(secret) : NotFound();
        }
            
        [HttpPost]
        public async Task<ActionResult> Post(Secret secret) 
        {
            _ctx.Chamber.Add(secret);
            await _ctx.SaveChangesAsync();
            return Created(secret.Id.ToString(), secret);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody]Secret secret)
        {
            var existing = await _ctx.Chamber.FirstOrDefaultAsync(s => s.Id == id);
            if (existing == null) return BadRequest();
            if (secret.Name != null) existing.Name = secret.Name;
            if (secret.Value != null) existing.Value = secret.Value;
            await _ctx.SaveChangesAsync();
            return Ok(existing);    
        }
    }
}