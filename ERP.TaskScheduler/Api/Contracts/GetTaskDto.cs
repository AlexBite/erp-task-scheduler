using ERP.TaskScheduler.Models;

namespace ERP.TaskScheduler.Api.Contracts;

public class GetTaskDto
{
    public int Id { get; set; }

    /// <summary>
    /// Интервал повторения задачи (если не задан, то задача будет выполнена только один раз)
    /// </summary>
    public int RepeatIntervalMinutes { get; set; } = 0;

    /// <summary>
    /// Дата запуска задачи
    /// </summary>
    public DateTime ExecuteAt { get; set; }
    
    public string Comment { get; set; } = string.Empty;
    public ScheduledTaskStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}