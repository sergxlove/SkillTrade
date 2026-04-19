namespace SkillTrade.DataAccess.Postgres.Models
{
    public class LessonsEntity
    {
        public Guid Id { get; set; }
        public Guid IdCourse { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
