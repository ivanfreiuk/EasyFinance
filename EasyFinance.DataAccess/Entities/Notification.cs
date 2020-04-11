namespace EasyFinance.DataAccess.Entities
{
    public class Notification
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool IsActive { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
