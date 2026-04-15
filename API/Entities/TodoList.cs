using System;

namespace API.Entities;

public class TodoList: AuditableEntity
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}
