using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIMPLE_WEB_API.Data;
using SIMPLE_WEB_API.Data.Entities;
using SIMPLE_WEB_API.repo;
using SIMPLE_WEB_API.ViewModel;

namespace SIMPLE_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repo;
        private readonly ApplicationDbContext _db;
        public CategoryController(ICategoryRepo repo, ApplicationDbContext db)
        {
            _repo = repo;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel data)
        {
            await _repo.CreateAsync(data);
            return CreatedAtAction(nameof(GetbyId), new { data.Id }, data);
        }
        [HttpGet]
        public async Task<IActionResult> GetbyId(Guid id)
        {
           var data = await _repo.GetById(id);
            return Ok(data);

        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryViewModel items)
        {
            //var result =  _repo.UpdateAsync(data);
            //return Ok(result);
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
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
