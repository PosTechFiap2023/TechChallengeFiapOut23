using StudentGroupsManager.DTO;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Interface;

public interface ICourseGroupRepository : IRepository<CourseGroup>
{
    IList<GroupGetDTO> GetAllWithDto();
    IList<GroupGetDTO> GetActiveListByCourse(int courseId);
    void ChangeMaxNumberOfStudents(int id, int numberOfStudents);
    void EnrollAGroup(int id);
    void UnEnrollAGroup(int groupId, int studentId);
    void CreateAGroup(GroupCreateDTO dto);
    void CloseGroupsWhenDeadlineReached(int courseId);
}