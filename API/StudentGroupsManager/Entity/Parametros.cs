using System.Text.Json.Serialization;

namespace StudentGroupsManager.Entity
{
    public class Parametros : BaseEntity
    {
        public DateTime GroupRegistrationDeadline { get; set; }

        public bool IsEsnabled { get; set; }

        public int CourseId { get; set; }

        [JsonIgnore]
        public Course Course { get; set; }
    }
}
