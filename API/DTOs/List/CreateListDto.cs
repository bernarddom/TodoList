using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.List;

public class CreateListDto
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";
}
