using System;
using System.Linq.Expressions;
using API.DTOs.Item;
using API.Entities;

namespace API.Mappers;

public static class ViewItemMapper
{
    public static Expression<Func<TodoItem, ViewItemDto>> ToDto =>
        todoItem => new ViewItemDto
        {
            Id = todoItem.Id,
            Value = todoItem.Value,
            Checkmark = todoItem.Checkmark
        };
}
