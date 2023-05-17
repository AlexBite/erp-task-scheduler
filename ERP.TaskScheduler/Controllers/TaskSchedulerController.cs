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

    [HttpGet("list")]
    public async Task<IActionResult> GetList()
    {
        var response = await _dbContext.Tasks.Select((t) => new GetTaskDto
        {
            Id = t.Id,
            RepeatIntervalMinutes = t.RepeatIntervalMinutes,
            ExecuteAt = t.ExecuteAt,
            Comment = t.Comment,
            Status = t.Status,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
        }).ToListAsync();

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var task = await _dbContext.FindAsync<ScheduledTask>(id);
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTaskDto createTaskDto)
    {
        if (createTaskDto == null)
            return BadRequest();

        var task = new ScheduledTask
        {
            RepeatIntervalMinutes = createTaskDto.RepeatIntervalMinutes,
            ExecuteAt = createTaskDto.ExecuteAt,
            Comment = createTaskDto.Comment,
            Status = ScheduledTaskStatus.WaitingForActivation,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync();

        return Ok(task);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = _dbContext.Attach(new ScheduledTask() { Id = id });
        task.State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("{id:int}/run")]
    public async Task<IActionResult> Run(int id)
    {
        var task = await _dbContext.FindAsync<ScheduledTask>(id);
        if (task is null)
            return NotFound();
        
        task.Status = ScheduledTaskStatus.WaitingToRun;
        task.ExecuteAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return Ok(task);
    }

    [HttpPost("test")]
    public async Task<ActionResult> Test()
    {
        var testTask = await _dbContext.Tasks.AddAsync(new ScheduledTask
        {
            RepeatIntervalMinutes = (int)TimeSpan.Parse("12:23:43").TotalMinutes,
            ExecuteAt = DateTime.UtcNow.AddDays(3),
            Comment = "Тестовая задача",
            Status = ScheduledTaskStatus.WaitingForActivation,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        });
        await _dbContext.SaveChangesAsync();

        return Ok(testTask);
    }
}