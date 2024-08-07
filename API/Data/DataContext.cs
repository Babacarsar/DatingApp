using Microsoft.EntityFrameworkCore;
using DatingApp.Entities;

namespace DatingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // Vos DbSets ici
        public DbSet<AppUser> Users { get; set; }
    }
}
