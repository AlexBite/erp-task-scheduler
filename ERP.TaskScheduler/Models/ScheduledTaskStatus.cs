namespace ERP.TaskScheduler.Models;

public enum ScheduledTaskStatus
{
    /// <summary>
    /// Задача ожидает активации
    /// </summary>
    WaitingForActivation = 0,
    
    /// <summary>
    /// Задача спланирована для запуска и ожидает времени начала выполнения
    /// </summary>
    WaitingToRun = 1,

    /// <summary>
    /// Задача запущена
    /// </summary>
    Running = 2,

    /// <summary>
    /// Задача успешно завершила выполнение
    /// </summary>
    Completed = 3,

    /// <summary>
    /// Задача неудачно завершила выполнение
    /// </summary>
    Faulted = 4
}