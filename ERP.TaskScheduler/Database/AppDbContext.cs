using Microsoft.EntityFrameworkCore;

namespace ERP.TaskScheduler.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}