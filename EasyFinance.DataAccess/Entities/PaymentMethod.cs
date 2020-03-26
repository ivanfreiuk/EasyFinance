namespace EasyFinance.DataAccess.Entities
{
   public class PaymentMethod
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MatchPattern { get; set; }
    }
}
