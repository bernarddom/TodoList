using System;
using System.Linq;
using System.Linq.Expressions;
using API.DTOs.List;
using API.Entities;

namespace API.Mappers.List;

public static class ViewListMapper
{
    public static Expression<Func<TodoList, ViewListDto>> ToDto()
    {
        
        return list => new ViewListDto
        {
            Id = list.Id ?? 0,
            Name = list.Name,
            Description = list.Description ?? "",
            TotalItems = list.TodoItems != null ? list.TodoItems.Count : 0,
            createdAt = list.CreatedAt,
            ItemsPreview = (list.TodoItems ?? Enumerable.Empty<TodoItem>()
                            ).AsQueryable()
                        .Where(i => i.ListId == list.Id)
                        .OrderBy(i => i.CreatedAt)
                        .Take(10)
                        .Select(ViewItemMapper.ToDto)
                        .ToList()
        };
    }
}
