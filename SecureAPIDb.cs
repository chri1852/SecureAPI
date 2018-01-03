using SecureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SecureAPI
{
    public class SecureAPIDb : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./SecureAPI.db");
        }
    }
}
