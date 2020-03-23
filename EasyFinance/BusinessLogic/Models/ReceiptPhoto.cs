using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFinance.BusinessLogic.Models
{
    public class ReceiptPhoto
    {
        public int Id { get; set; }

        public int? ReceiptId { get; set; }

        public string FileName { get; set; }

        public byte[] FileBytes { get; set; }
    }
}
