using System;
using API.DTOs.Item;

namespace API.DTOs.List;

public class ViewListDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }

    // public ICollection<ViewItemDto>? Items {get;set;}
}
