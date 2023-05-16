using ERP.TaskScheduler.Api.Contracts;
using ERP.TaskScheduler.Database;
using ERP.TaskScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.TaskScheduler.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskSchedulerController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public TaskSchedulerController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTaskDto>>> GetTasks()
    {
        return await _dbContext.Tasks.Select((t) => new GetTaskDto
        {
            Id = t.Id,
            RepeatIntervalMinutes = t.RepeatIntervalMinutes,
            ExecuteAt = t.ExecuteAt,
            Comment = t.Comment,
            Status = t.Status,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
        }).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult> AddQuestion([FromBody] CreateTaskDto createTaskDto)
    {
        if (createTaskDto == null)
            return BadRequest();

        var task = new ScheduledTask
        {
            RepeatIntervalMinutes = createTaskDto.RepeatIntervalMinutes,
            ExecuteAt = createTaskDto.ExecuteAt,
            Comment = createTaskDto.Comment,
            Status = ScheduledTaskStatus.Disabled,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("test")]
    public async Task<ActionResult> Test()
    {
        await _dbContext.Tasks.AddAsync(new ScheduledTask
        {
            RepeatIntervalMinutes = (int)TimeSpan.Parse("12:23:43").TotalMinutes,
            ExecuteAt = DateTime.UtcNow.AddDays(3),
            Comment = "Тестовая задача",
            Status = ScheduledTaskStatus.Disabled,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        });
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}