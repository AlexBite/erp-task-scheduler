namespace ERP.TaskScheduler.Models;

public enum ScheduledTaskStatus
{
    /// <summary>
    /// Задача ожидает активации
    /// </summary>
    Disabled,
    
    /// <summary>
    /// Задача спланирована для запуска и ожидает времени начала выполнения
    /// </summary>
    WaitingToRun,

    /// <summary>
    /// Задача запущена
    /// </summary>
    Running,

    /// <summary>
    /// Задача успешно завершила выполнение
    /// </summary>
    Completed,

    /// <summary>
    /// Задача неудачно завершила выполнение
    /// </summary>
    Faulted
}