using SkillTrade.Core.Infrastructures;
using System.Reflection.PortableExecutable;

namespace SkillTrade.Core.Models
{
    public class Courses
    {
        public const int MIN_LENGTH_TITLE = 3;
        public const int MAX_LENGTH_TITLE = 200;
        public const int MIN_LENGTH_DESCRIPTION = 10;
        public const int MAX_LENGTH_DESCRIPTION = 5000;
        public const int MAX_PRICE = 100000;
        public const int MAX_LESSONS_COUNT = 5000;
        public const int MAX_DURATION_HOURS = 500;
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
            if (title.Length < MIN_LENGTH_TITLE)
                return ResultModel<Courses>.Failure($"Поле Title должно содержать минимум {MIN_LENGTH_TITLE} символа");
            if (title.Length > MAX_LENGTH_TITLE)
                return ResultModel<Courses>.Failure($"Поле Title не должно превышать {MAX_LENGTH_TITLE} символов");
            if (string.IsNullOrWhiteSpace(description))
                return ResultModel<Courses>.Failure("Поле Description не должно быть пустым");
            if (description.Length < MIN_LENGTH_DESCRIPTION)
                return ResultModel<Courses>.Failure($"Поле Description должно содержать минимум {MIN_LENGTH_DESCRIPTION} символов");
            if (description.Length > MAX_LENGTH_DESCRIPTION)
                return ResultModel<Courses>.Failure($"Поле Description не должно превышать {MAX_LENGTH_DESCRIPTION} символов");
            if (string.IsNullOrWhiteSpace(level))
                return ResultModel<Courses>.Failure("Поле Level не должно быть пустым");
            if (price < 0)
                return ResultModel<Courses>.Failure("Поле Price не должно быть отрицательным");
            if (price > MAX_PRICE)
                return ResultModel<Courses>.Failure($"Поле Price не должно превышать {MAX_PRICE}");
            if (lessonsCount <= 0)
                return ResultModel<Courses>.Failure("Поле LessonsCount должно быть больше 0");
            if (lessonsCount > MAX_LESSONS_COUNT)
                return ResultModel<Courses>.Failure($"Поле LessonsCount не должно превышать {MAX_LESSONS_COUNT}");
            if (durationTimeHours <= 0)
                return ResultModel<Courses>.Failure("Поле DurationTimeHours должно быть больше 0");
            if (durationTimeHours > MAX_DURATION_HOURS)
                return ResultModel<Courses>.Failure($"Поле DurationTimeHours не должно превышать {MAX_DURATION_HOURS} часов");
            if (createdAt > DateTime.UtcNow)
                return ResultModel<Courses>.Failure("Поле CreatedAt не может быть в будущем");
            return ResultModel<Courses>.Success(new Courses(id, idActor, title, description, level,
                price, lessonsCount, durationTimeHours, createdAt));
        }
    }
}
