using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Configurations;

public class CourseGroupConfiguration : IEntityTypeConfiguration<CourseGroup>
{
    public void Configure(EntityTypeBuilder<CourseGroup> builder)
    {
        builder.ToTable("Groups");
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
        builder.Property(g => g.CreatorId).HasColumnType("INT");
        builder.Property(g => g.CourseId).HasColumnType("INT");
        builder.Property(g => g.Name).HasColumnType("VARCHAR(100)");
        builder.Property(g => g.MaxNumberOfStudents).HasColumnType("INT").HasDefaultValue(5);
        builder.Property(g => g.StudentsJoined).HasColumnType("INT");
        builder.Property(g => g.IsClosed).HasColumnType("BIT").HasDefaultValue(0);
        builder.HasOne(g => g.Creator)
            .WithMany(s => s.GroupsOwner)
            .HasForeignKey(g => g.CreatorId);
        builder.HasMany(g => g.Students)
            .WithOne(sg => sg.Group)
            .HasPrincipalKey(g => g.Id);
    }
}