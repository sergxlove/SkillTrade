using SkillTrade.Core.Infrastructures;

namespace SkillTrade.Core.Models
{
    public class VerifyOperations
    {
        public Guid Id { get; }
        public string Email { get; } = string.Empty;
        public string Code { get; } = string.Empty;
        public DateTime DateCreate { get; }
        public int QuantityTry { get; } = 0;
        public MailType TypeOperation { get; }

        private VerifyOperations(Guid id, string email, string code, DateTime dateCreate, int quantityTry,
            MailType typeOperation)
        {
            Id = id;
            Email = email;
            Code = code;
            DateCreate = dateCreate;
            QuantityTry = quantityTry;
            TypeOperation = typeOperation;
        }

        public static ResultModel<VerifyOperations> Create(Guid id, string email, string code, 
            DateTime dateCreate, int quantityTry, MailType typeOperation)
        {
            if (id == Guid.Empty)
                return ResultModel<VerifyOperations>.Failure("Поле Id не должно быть пустым");
            if (email == string.Empty)
                return ResultModel<VerifyOperations>.Failure("Поле Email не должно быть пустым");
            if (code == string.Empty)
                return ResultModel<VerifyOperations>.Failure("Поле Code не должно быть пустым");
            return ResultModel<VerifyOperations>.Success(new VerifyOperations(id, email, code, 
                dateCreate, quantityTry, typeOperation));
        }

        public static ResultModel<VerifyOperations> Create(string email, string code, DateTime dateCreate, 
            int quantityTry, MailType typeOperation)
        {
            return Create(Guid.NewGuid(), email, code, dateCreate, quantityTry, typeOperation);
        }
    }
}
