using System;
using API.DTOs.Item;

namespace API.DTOs.List;

public class ViewListsDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}
