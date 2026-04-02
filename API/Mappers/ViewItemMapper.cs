using System;
using API.DTOs.Item;
using API.Entities;

namespace API.Mappers;

public static class ViewItemMapper
{
    public static ViewItemDto ToDto(this TodoItem todoItem)
    {
        return new ViewItemDto
        {
            Value = todoItem.Value,
            Checkmark = todoItem.Checkmark
        };
    }
}
