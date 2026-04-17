using SkillTrade.Core.Infrastructures;

namespace SkillTrade.Core.Models
{
    public class UserCourses
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public Guid CourseId { get; }
        public int CurrentProgress { get; }
        public int TotalProgress { get; }
        public DateTime SubscribeTime { get; }

        private UserCourses(Guid id, Guid userId, Guid courseId, int currentProgress, int totalProgress,
            DateTime subscribeTime)
        {
            Id = id;
            UserId = userId;
            CourseId = courseId;
            CurrentProgress = currentProgress;
            TotalProgress = totalProgress;
            SubscribeTime = subscribeTime;
        }

        public static ResultModel<UserCourses> Create(Guid id, Guid userId, Guid courseId, int currentProgress, int totalProgress,
            DateTime subscribeTime)
        {
            if (id == Guid.Empty)
                return ResultModel<UserCourses>.Failure("Поле Id не должно быть пустым");
            if (userId == Guid.Empty)
                return ResultModel<UserCourses>.Failure("Поле UserId не должно быть пустым");
            if (courseId == Guid.Empty)
                return ResultModel<UserCourses>.Failure("Поле CourseId не должно быть пустым");
            if (currentProgress < 0)
                return ResultModel<UserCourses>.Failure("Поле CurrentProgress не должно быть отрицательным");
            if (currentProgress > totalProgress)
                return ResultModel<UserCourses>.Failure("Поле CurrentProgress не может быть больше TotalProgress");
            if (totalProgress < 0)
                return ResultModel<UserCourses>.Failure("Поле TotalProgress должно быть больше 0");
            if (totalProgress > 10000)
                return ResultModel<UserCourses>.Failure("Поле TotalProgress не должно превышать 10000");
            if (subscribeTime > DateTime.UtcNow)
                return ResultModel<UserCourses>.Failure("Поле SubscribeTime не может быть в будущем");
            return ResultModel<UserCourses>.Success(new UserCourses(id, userId, courseId, currentProgress,
                totalProgress, subscribeTime));
        }
    }
}
