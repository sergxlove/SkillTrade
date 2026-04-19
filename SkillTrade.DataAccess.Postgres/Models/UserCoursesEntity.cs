namespace SkillTrade.DataAccess.Postgres.Models
{
    public class UserCoursesEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public int CurrentProgress { get; set; }
        public int TotalProgress { get; set; }
        public DateTime SubscribeTime { get; set; }
    }
}
