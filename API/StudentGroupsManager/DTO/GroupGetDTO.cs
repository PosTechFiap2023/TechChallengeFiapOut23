namespace StudentGroupsManager.DTO;

public class GroupGetDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxNumberOfStudents { get; set; }
    public int StudentsJoined { get; set; }
    public string CourseName { get; set; }
    public string CreatorName { get; set; }
}