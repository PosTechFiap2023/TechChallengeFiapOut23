using Newtonsoft.Json;

namespace StudentGroupsManager.Entity
{
    public class Course : BaseEntity
    {
        public string NameCourse { get; set; }
        [JsonIgnore]
        public ICollection<CourseGroup> Groups { get; set; }
    }
}
