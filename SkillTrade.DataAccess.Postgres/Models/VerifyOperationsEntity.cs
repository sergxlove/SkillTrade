namespace SkillTrade.DataAccess.Postgres.Models
{
    public class VerifyOperationsEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime DateCreate { get; set; }
        public int QuantityTry { get; set; }
    }
}
