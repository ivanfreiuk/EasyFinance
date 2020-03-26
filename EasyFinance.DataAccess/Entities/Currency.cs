namespace EasyFinance.DataAccess.Entities
{
    public class Currency
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public string MatchPattern { get; set; }

        public string GenericCode { get; set; }
    }
}
