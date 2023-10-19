using Microsoft.EntityFrameworkCore;
using UserData.Entitys;

namespace UserData.Data
{
    public class UserDataDbContext : DbContext
    {
        public DbSet<User> Employees => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("UserDataDb");
        }
    }
}

