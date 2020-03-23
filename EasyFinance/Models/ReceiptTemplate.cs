using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFinance.Models
{
    public class ReceiptTemplate
    {
        public Section HeaderSection { get; set; }

        public Section ProductListSection { get; set; }

        public Section FooterSection { get; set; }
    }
}
