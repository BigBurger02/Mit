using Microsoft.EntityFrameworkCore;
using MIT_1.Model;

namespace MIT_1.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
}