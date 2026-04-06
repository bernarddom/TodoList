using System;
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
            Name = list.Name,
            Description = list.Description ?? ""
        };
    }
}
