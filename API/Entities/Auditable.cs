using System;

namespace API.Entities;

public abstract class Auditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    // public string CreatedBy { get; set; }

    // public string UpdatedBy { get; set; }
}