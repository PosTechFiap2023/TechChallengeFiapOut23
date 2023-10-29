using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Interface;

public interface ICourseGroupRepository : IRepository<CourseGroup>
{
    IList<CourseGroup> GetAll();
    IList<CourseGroup> GetActiveListByCourse(int courseId);
    void ChangeMaxNumberOfStudents(int id, int numberOfStudents);
    void EnrollAGroup(int id);
}