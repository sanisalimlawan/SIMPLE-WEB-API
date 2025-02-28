using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIMPLE_WEB_API.Data;
using SIMPLE_WEB_API.Data.Entities;
using SIMPLE_WEB_API.Paginationfilter;

namespace SIMPLE_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category data)
        {
            await _db.categories.AddAsync(data);
            await _db.TrySaveChangesAsync();
            return CreatedAtAction(nameof(GetbyId), new { data.Id }, data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(Guid id)
        {
           var data = await _db.categories.FindAsync(id);
            return Ok(data);

        }

        [HttpPut]
        public async Task<IActionResult> Update(Category items)
        {
            var check = await _db.categories.FindAsync(items.Id);
            if (check == null)
            {
                return NotFound("id not found");
            }

            check.Id = items.Id;
            check.Description = items.Description;
            check.Name = items.Name;
            _db.categories.Update(check);
            await _db.TrySaveChangesAsync();
            return Ok(check);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
           var getid = await _db.categories.FindAsync(id);
            if (getid == null)
            {
                return NotFound("id not found");
            }
            _db.categories.Remove(getid);
            await _db.TrySaveChangesAsync();
            return NoContent();
        }

        [HttpGet("PAginatedList")]
        public async Task<ActionResult<IEnumerable<Category>>> getAll([FromQuery] FilterOPtions options)
        {
            var getall = _db.categories.AsNoTracking();
            var count = await getall.CountAsync();
            var items = await getall.Skip((options.PageIndex - 1) * options.PageSize)
                .Take(options.PageSize).ToListAsync();
            var dat = PaginatedList<Category>.Create(items, count, options);
            return Ok(dat);
            
        }
    }
}
