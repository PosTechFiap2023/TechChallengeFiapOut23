using Newtonsoft.Json;

namespace StudentGroupsManager.Entity;

public class StudentGroup : BaseEntity
{
    public int GroupId { get; set; }
    public int StudentId { get; set; }
    [JsonIgnore]
    public Student Student { get; set; }
    [JsonIgnore] 
    public CourseGroup Group { get; set; }
}