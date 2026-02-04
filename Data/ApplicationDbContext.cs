using ClusterConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace ClusterConnect.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Project entity
        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("projects");
            
            entity.Property(e => e.Id)
                .HasColumnName("id");
            
            entity.Property(e => e.Title)
                .HasColumnName("title")
                .IsRequired();
            
            entity.Property(e => e.Description)
                .HasColumnName("description");
            
            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasDefaultValue("PLANNING");
            
            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
            
            entity.Property(e => e.TechStack)
                .HasColumnName("tech_stack");
            
            entity.Property(e => e.TeamSize)
                .HasColumnName("team_size");
            
            entity.Property(e => e.Category)
                .HasColumnName("category");
            
            entity.Property(e => e.IsPublic)
                .HasColumnName("is_public")
                .HasDefaultValue(true);

            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.Category);
        });
    }
}
