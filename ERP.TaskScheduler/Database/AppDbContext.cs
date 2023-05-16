using ERP.TaskScheduler.Models;
using Microsoft.EntityFrameworkCore;
using TaskStatus = ERP.TaskScheduler.Models.TaskStatus;

namespace ERP.TaskScheduler.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<ScheduledTask> Tasks { get; set; }
    public DbSet<TaskStatus> History { get; set; }
}