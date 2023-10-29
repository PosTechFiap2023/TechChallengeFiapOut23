using StudentGroupsManager.DTO;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Interface;

public interface ICourseGroupRepository : IRepository<CourseGroup>
{
    IList<GroupGetDTO> GetAll();
    IList<GroupGetDTO> GetActiveListByCourse(int courseId);
    void ChangeMaxNumberOfStudents(int id, int numberOfStudents);
    void EnrollAGroup(int id);
    void CreateAGroup(GroupCreateDTO dto);
}