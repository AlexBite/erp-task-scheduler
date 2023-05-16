using ERP.TaskScheduler.Models;

namespace ERP.TaskScheduler.Api.Contracts;

public class CreateTaskDto
{
    /// <summary>
    /// Интервал повторения задачи (если не задан, то задача будет выполнена только один раз)
    /// </summary>
    public int RepeatIntervalMinutes { get; set; } = 0;

    /// <summary>
    /// Дата запуска задачи
    /// </summary>
    public DateTime ExecuteAt { get; set; } = DateTime.MinValue;
    
    public string Comment { get; set; } = string.Empty;
}