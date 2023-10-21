using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Data
{
    public class StudentGroupsManagerContext : DbContext
    {
        public StudentGroupsManagerContext (DbContextOptions<StudentGroupsManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; } = default!;

        public DbSet<Student> Students { get; set; } = default!;

        public DbSet<TeacherCoordinator> TeacherCoordinators { get; set; } = default!;
    }
}
