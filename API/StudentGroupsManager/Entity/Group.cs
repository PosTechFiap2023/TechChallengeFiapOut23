namespace StudentGroupsManager.Entity
{
    public class Group : BaseEntity
    {
        public string NameGroup { get; set; }

        public int NumberMembers { get; set; }

        public List<Student> Members { get; set; }
    }
}
