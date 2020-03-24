using System;
using System.Collections.Generic;
using System.Text;

namespace EasyFinance.DataAccess.Entities
{
    public class Currency
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string GenericCode { get; set; }
    }
}
