using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Item;

public class CreateItemDto
{
    [Required]
    public string Value { get; set; } = "";
}
