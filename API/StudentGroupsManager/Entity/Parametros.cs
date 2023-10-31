using System.Text.Json.Serialization;

namespace StudentGroupsManager.Entity
{
    public class Parametros : BaseEntity
    {
        public DateTime GroupRegistrationDeadline { get; set; }

        public bool IsEsnabled { get; set; }

        public int GroupId { get; set; }

        [JsonIgnore]
        public CourseGroup Group { get; set; }
    }
}
