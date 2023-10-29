using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
        builder.Property(c => c.NameCourse).HasColumnType("VARCHAR(100)");
        builder.HasMany(c => c.Groups)
            .WithOne(g => g.Course)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}