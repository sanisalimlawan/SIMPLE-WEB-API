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
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product items)
        {
            Product product = new Product();
            product.Id = Guid.NewGuid();
            product.Title = items.Title;
            product.Description = items.Description;
            product.CategoryId = items.CategoryId;
            await _db.products.AddAsync(product);
            await _db.TrySaveChangesAsync();
            return CreatedAtAction(nameof(GetbyId), new {id = product.Id}, product);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetbyId(Guid Id)
        {
            var data = await _db.products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == Id);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> UPdate(Product product)
        {
            var chek = await _db.products.FindAsync(product.Id);
            if(chek == null)
            {
                return NotFound("product does not exist");
            }
            chek.Id = product.Id;
            chek.Sale = product.Sale;
            chek.Title = product.Title;
            chek.Description = product.Description;
            chek.Stock = product.Stock;
            chek.CategoryId = product.CategoryId;
            _db.products.Update(chek);
            await _db.TrySaveChangesAsync();
            return Ok(chek);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var gid = await _db.products.FindAsync(Id);
            if(gid == null)
            {
                return NotFound("product not found");
            }

            _db.products.Remove(gid);
            await _db.TrySaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _db.products.Include(x => x.Category).ToListAsync();
            return Ok(data);
        }

        [HttpGet("/Paginated")]
        public async Task<ActionResult<PaginatedList<IEnumerable<Product>>>> GetPaginatedList([FromQuery] FilterOPtions filter)
        {
            var data = _db.products.Include(x => x.Category).AsNoTracking();
            var count = await data.CountAsync();
            var items = await data.Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var get = PaginatedList<Product>.Create(items, count, filter);
            return Ok(get);
        }
    }
}
