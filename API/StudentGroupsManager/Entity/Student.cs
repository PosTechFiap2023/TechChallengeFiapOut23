namespace StudentGroupsManager.Entity
{

    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string RA { get; set; }
        public string Password { get; set; }
    }
}
