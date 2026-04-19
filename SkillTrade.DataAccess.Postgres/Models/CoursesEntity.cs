namespace SkillTrade.DataAccess.Postgres.Models
{
    public class CoursesEntity
    {
        public Guid Id { get; set; }
        public Guid IdActor { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int LessonsCount { get; set; }
        public int DurationTimeHours { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
