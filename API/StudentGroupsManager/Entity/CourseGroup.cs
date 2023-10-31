using Newtonsoft.Json;

namespace StudentGroupsManager.Entity;

public class CourseGroup : BaseEntity
{
    public int CourseId { get; set; }
    public int CreatorId { get; set; }
    public string Name { get; set; }
    public int MaxNumberOfStudents { get; set; } = 5;
    public int StudentsJoined { get; set; } = 1;
    public bool IsClosed { get; set; } = false;
    [JsonIgnore]
    public Course Course { get; set; }
    [JsonIgnore]
    public Student Creator { get; set; }
    [JsonIgnore] 
    public ICollection<StudentGroup> Students { get; set; }
}