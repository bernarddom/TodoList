using System;

namespace API.Entities;

public class TodoItem: AuditableEntity
{
    public int? Id { get; set; }
    public required string Value { get; set; }
    public bool Checkmark { get; set; } = false;
    public required int ListId { get; set; }
    public TodoList? List { get; set; }
}
