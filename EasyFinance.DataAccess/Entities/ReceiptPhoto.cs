namespace EasyFinance.DataAccess.Entities
{
    public class ReceiptPhoto
    {
        public int Id { get; set; }
        
        public string FileName { get; set; }

        public byte[] FileBytes { get; set; }
    }
}
