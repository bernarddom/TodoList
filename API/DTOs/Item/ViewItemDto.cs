using System;

namespace API.DTOs.Item;

public class ViewItemDto
{
    public int? Id { get; set; }
    public required string Value { get; set; }

    public required bool Checkmark { get; set; }
}
