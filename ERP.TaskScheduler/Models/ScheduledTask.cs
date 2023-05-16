using System.ComponentModel.DataAnnotations;

namespace ERP.TaskScheduler.Models;

public class ScheduledTask
{
    [Key] public int Id { get; set; }
    
    /// <summary>
    /// Интервал повторения задачи (если не задан, то задача будет выполнена только один раз)
    /// </summary>
    public TimeSpan RepeatInterval { get; set; }
    
    /// <summary>
    /// Запланированная дата запуска задачи
    /// </summary>
    public DateTime ExecuteAt { get; set; }
    
    public string Comment { get; set; } = string.Empty;
    public ScheduledTaskStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// История статусов задачи
    /// </summary>
    public ICollection<TaskStatus> History { get; set; } = new List<TaskStatus>();
}