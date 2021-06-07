namespace Data.Entities
{
    public class StaffSkill : BaseEntity
    {
        public string Username { get; set; }

        public User User { get; set; }

        public int SkillId { get; set; }

        public Skill Skill { get; set; }

        public int Level { get; set; }
    }
}
