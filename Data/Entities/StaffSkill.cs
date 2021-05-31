namespace Data.Entities
{
    public class StaffSkill
    {
        public int StaffId { get; set; }

        public Staff Staff { get; set; }

        public int SkillId { get; set; }

        public Skill Skill { get; set; }

        public int Level { get; set; }
    }
}
