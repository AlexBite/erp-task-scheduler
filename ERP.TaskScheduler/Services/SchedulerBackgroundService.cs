using ERP.TaskScheduler.Database;
using ERP.TaskScheduler.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.TaskScheduler.Services;

public class SchedulerBackgroundService : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromMinutes(1);
    private readonly ILogger<SchedulerBackgroundService> _logger;
    private readonly IServiceScopeFactory _factory;
    public bool IsEnabled { get; set; }

    public SchedulerBackgroundService(
        ILogger<SchedulerBackgroundService> logger,
        IServiceScopeFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_period);
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await RunWaitingTasks(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Failed to execute SchedulerBackgroundService: {Message}", e.Message);
            }
        }
    }

    private async Task RunWaitingTasks(CancellationToken stoppingToken)
    {
        if (!IsEnabled)
            return;
        
        await using var asyncScope = _factory.CreateAsyncScope();
        var dbContext = asyncScope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        var waitingTasks = await dbContext.Tasks
            .Where((t) => t.Status == ScheduledTaskStatus.WaitingToRun)
            .Where((t) => t.ExecuteAt <= DateTime.UtcNow)
            .ToListAsync(cancellationToken: stoppingToken);
                
        //TODO: Отправить таски в реббит

        foreach (var t in waitingTasks)
            t.Status = ScheduledTaskStatus.Running;

        await dbContext.SaveChangesAsync(stoppingToken);
    }
}