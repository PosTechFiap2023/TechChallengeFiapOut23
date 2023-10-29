using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
        builder.Property(s => s.Name).HasColumnType("VARCHAR(100)");
        builder.Property(s => s.Mail).HasColumnType("VARCHAR(100)");
        builder.Property(s => s.RA).HasColumnType("VARCHAR(100)");
        builder.Property(s => s.Password).HasColumnType("VARCHAR(100)");
        builder.HasMany(s => s.GroupsOwner)
            .WithOne(g => g.Creator)
            .HasPrincipalKey(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(s => s.GroupsEnrolled)
            .WithOne(sg => sg.Student)
            .HasPrincipalKey(s => s.Id);
    }
}