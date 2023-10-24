using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Services
{
    public interface ITokenService
    {
        string GenerateTokenStudent(Student student);
        string GenerateTokenTeacherCoordinator(TeacherCoordinator teacherCoordinator);
    }
}