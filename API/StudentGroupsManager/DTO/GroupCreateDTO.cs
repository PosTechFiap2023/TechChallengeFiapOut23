namespace StudentGroupsManager.DTO;

public class GroupCreateDTO
{
    public int CourseId { get; set; }
    public int CreatorId { get; set; }
    public string Name { get; set; }
    public int? MaxNumberOfStudents { get; set; }
}