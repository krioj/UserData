using Microsoft.EntityFrameworkCore;
using UserData.Entitys;

namespace UserData.Data;

public class UserDataDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=WALERA;Initial Catalog=UsersDb;Integrated Security=True; TrustServerCertificate=True");
    }
}

