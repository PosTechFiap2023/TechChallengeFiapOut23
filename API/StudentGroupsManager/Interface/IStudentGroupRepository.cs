using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Interface;

public interface IStudentGroupRepository
{
    IList<StudentGroup> GetListStudentsByGroup(int groupId);
    void EnrollStudent(int groupId, int studentId);
    void UnEnrollStudent(int groupId, int studentId);
    Task UnEnrollAllStudents(int groupId);
}