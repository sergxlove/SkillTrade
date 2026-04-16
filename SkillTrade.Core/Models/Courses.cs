using SkillTrade.Core.Infrastructures;

namespace SkillTrade.Core.Models
{
    public class Courses
    {
        public Guid Id { get; }
        public Guid IdActor { get; }
        public string Title { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public string Level { get; } = string.Empty;
        public decimal Price { get; }
        public int LessonsCount { get; }
        public int DurationTimeHours { get; }
        public DateTime CreatedAt { get; }

        private Courses(Guid id, Guid idActor, string title, string description, string level, decimal price,
            int lessonsCount, int durationTimeHours, DateTime createdAt)
        {
            Id = id;
            IdActor = idActor;
            Title = title;
            Description = description;
            Level = level;
            Price = price;
            LessonsCount = lessonsCount;
            DurationTimeHours = durationTimeHours;
            CreatedAt = createdAt;
        }

        public static ResultModel<Courses> Create(Guid id, Guid idActor, string title, string description, string level, decimal price,
            int lessonsCount, int durationTimeHours, DateTime createdAt)
        {
            if (id == Guid.Empty)
                return ResultModel<Courses>.Failure("Поле Id не должно быть пустым");
            if (idActor == Guid.Empty)
                return ResultModel<Courses>.Failure("Поле IdActor не должно быть пустым");
            if (string.IsNullOrWhiteSpace(title))
                return ResultModel<Courses>.Failure("Поле Title не должно быть пустым");
            if (title.Length < 3)
                return ResultModel<Courses>.Failure("Поле Title должно содержать минимум 3 символа");
            if (title.Length > 200)
                return ResultModel<Courses>.Failure("Поле Title не должно превышать 200 символов");
            if (string.IsNullOrWhiteSpace(description))
                return ResultModel<Courses>.Failure("Поле Description не должно быть пустым");
            if (description.Length < 10)
                return ResultModel<Courses>.Failure("Поле Description должно содержать минимум 10 символов");
            if (description.Length > 5000)
                return ResultModel<Courses>.Failure("Поле Description не должно превышать 5000 символов");
            if (string.IsNullOrWhiteSpace(level))
                return ResultModel<Courses>.Failure("Поле Level не должно быть пустым");
            if (price < 0)
                return ResultModel<Courses>.Failure("Поле Price не должно быть отрицательным");
            if (price > 100000)
                return ResultModel<Courses>.Failure("Поле Price не должно превышать 100 000");
            if (lessonsCount <= 0)
                return ResultModel<Courses>.Failure("Поле LessonsCount должно быть больше 0");
            if (lessonsCount > 1000)
                return ResultModel<Courses>.Failure("Поле LessonsCount не должно превышать 1000");
            if (durationTimeHours <= 0)
                return ResultModel<Courses>.Failure("Поле DurationTimeHours должно быть больше 0");
            if (durationTimeHours > 500)
                return ResultModel<Courses>.Failure("Поле DurationTimeHours не должно превышать 500 часов");
            if (createdAt > DateTime.UtcNow)
                return ResultModel<Courses>.Failure("Поле CreatedAt не может быть в будущем");
            return ResultModel<Courses>.Success(new Courses(id, idActor, title, description, level,
                price, lessonsCount, durationTimeHours, createdAt));
        }
    }
}
