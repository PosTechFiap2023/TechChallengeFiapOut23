using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Configurations;

public class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
{
    public void Configure(EntityTypeBuilder<StudentGroup> builder)
    {
        builder.ToTable("StudentsGroups");
        builder.HasKey(sg => sg.Id);
        builder.Property(sg => sg.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
        builder.Property(sg => sg.GroupId).HasColumnType("INT");
        builder.Property(sg => sg.StudentId).HasColumnType("INT");
        builder.HasOne(sg => sg.Group)
            .WithMany(g => g.Students)
            .HasForeignKey(sg => sg.GroupId);
        builder.HasOne(sg => sg.Student)
            .WithMany(s => s.GroupsEnrolled)
            .HasForeignKey(sg => sg.StudentId);
    }
}