using System;
using System.Linq.Expressions;
using API.DTOs.List;
using API.Entities;

namespace API.Mappers.List;

public static class ViewListsMapper
{
    public static Expression<Func<TodoList, ViewListsDto>> ToDto()
    {
        
        return list => new ViewListsDto
        {
            Id = list.Id ?? 0,
            Name = list.Name,
            Description = list.Description ?? ""
        };
    }
}
