using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class DataContext : DbContext, IDataContext
    {
        public DbSet<Person> People { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

    }
}
