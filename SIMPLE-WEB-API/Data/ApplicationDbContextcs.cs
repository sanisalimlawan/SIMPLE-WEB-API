using Microsoft.EntityFrameworkCore;
using SIMPLE_WEB_API.Data.Entities;

namespace SIMPLE_WEB_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        //public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Category> categories { get; set; }

        public async Task<bool> TrySaveChangesAsync()
        {
            try
            {
                await SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
