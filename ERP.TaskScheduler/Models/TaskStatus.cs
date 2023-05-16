using System.ComponentModel.DataAnnotations;

namespace ERP.TaskScheduler.Models;

public class TaskStatus
{
    [Key] public int Id { get; set; }
    public int TaskId { get; set; }
    public ScheduledTask Task { get; set; } = null!;
    public ScheduledTaskStatus Status { get; set; }
    public DateTime UpdatedAt { get; set; }
}