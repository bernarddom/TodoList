using System;
using System.Linq.Expressions;
using System.Linq;
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
            TotalItems = list.TodoItems?.Count ?? 0,
            createdAt = list.CreatedAt,
            ItemsPreview = (list.TodoItems?.AsEnumerable() ?? Enumerable.Empty<TodoItems>())
                    .OrderBy(i => i.CreatedAt)
                    .Take(10)
                    .Select(ViewItemMapper.ToDto())
                    .ToList()
        };
    }
}
