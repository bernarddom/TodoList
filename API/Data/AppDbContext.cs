using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{

    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<TodoList> TodoLists { get; set; }

    public override int SaveChanges()
    {
        ApplyAuditing();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditing();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditing()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AuditableEntity &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        var now = DateTime.UtcNow;
        // var user = _currentUser?.UserId ?? "system";

        foreach (var entry in entries)
        {
            var entity = (AuditableEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = now;
                // entity.CreatedBy = user;
            }

            if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = now;
                // entity.UpdatedBy = user;

                entry.Property(nameof(AuditableEntity.CreatedAt)).IsModified = false;
                // entry.Property(nameof(AuditableEntity.CreatedBy)).IsModified = false;
            }
        }
    }
}
