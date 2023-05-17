using ERP.TaskScheduler.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.TaskScheduler.Controllers;

[ApiController]
[Route("api/service")]
public class ServiceController : ControllerBase
{
    private readonly SchedulerBackgroundService _bgService;

    public ServiceController(SchedulerBackgroundService bgService)
    {
        _bgService = bgService;
    }
    
    [HttpPost("enable")]
    public async Task<IActionResult> Enable()
    {
        _bgService.IsEnabled = true;
        return Ok();
    }

    [HttpPost("disable")]
    public async Task<IActionResult> Disable()
    {
        _bgService.IsEnabled = false;
        return Ok();
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetStatus()
    {
        return Ok(_bgService.IsEnabled);
    }

    [HttpGet("/ping")]
    public async Task<IActionResult> Ping()
    {
        return Ok();
    }
}