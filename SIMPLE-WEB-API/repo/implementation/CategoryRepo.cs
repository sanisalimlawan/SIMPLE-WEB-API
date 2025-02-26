using Microsoft.EntityFrameworkCore;
using SIMPLE_WEB_API.Data;
using SIMPLE_WEB_API.Data.Entities;
using SIMPLE_WEB_API.ViewModel;

namespace SIMPLE_WEB_API.repo.implementation
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(CategoryViewModel items)
        {
            Category category = new Category
            {
                Name = items.Name,
                Description = items.Description,
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };
            await _context.categories.AddAsync(category);
            await _context.TrySaveChangesAsync();

            //return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var getid = await _context.categories.FindAsync(id);
            if(getid == null)
            {
                return;
            }
            _context.categories.Remove(getid);
            await _context.SaveChangesAsync();
        }

        public async Task GetAllAsync()
        {
            var data = await _context.categories.ToListAsync();
            //return data;
        }

        public async Task<CategoryViewModel?> GetById(Guid id)
        {
            var data = await _context.categories.FirstOrDefaultAsync(c => c.Id == id);
            if(data == null)
            {
                return null;
            }

            return new CategoryViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
            };
        }

        public async Task UpdateAsync(CategoryViewModel items)
        {
            var check = await _context.categories.FindAsync(items.Id);
            if (check == null)
            {
                
            }

            check.Id = items.Id;
            check.Description = items.Description;
            check.Name = items.Name;
            _context.categories.Update(check);
            await _context.TrySaveChangesAsync();



        }
    }
}
