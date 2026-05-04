namespace SkillTrade.CoursesAPI.Requests
{
    public class SubscribeRequest
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public int TotalProgress { get; set; }
    }
}
