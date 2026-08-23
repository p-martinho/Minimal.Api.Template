namespace Minimal.Api.Template.Domain.ValueObjects.TodoLists;

/// <summary>
/// The to do item schedule.
/// </summary>
/// <param name="DueDate">The due date.</param>
public record TodoItemSchedule(DateTimeOffset? DueDate);