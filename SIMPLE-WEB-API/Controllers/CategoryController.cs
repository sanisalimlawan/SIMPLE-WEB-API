using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CategoryController(ICategoryRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel data)
        {
            await _repo.CreateAsync(data);
            return CreatedAtAction(nameof(GetbyId), new { data.Id }, data);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetbyId(Guid id)
        {
            //if(id >= 0 )
            //{
            //    return NotBa("id not found in db");
            //}
           var data = await _repo.GetById(id);
            return Ok(data);

        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var data = await _repo.GetAllAsync();
        //    return Ok();
        //}
    }
}
