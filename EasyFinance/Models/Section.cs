using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFinance.Models
{
    public class Section
    {
        public string SectionName { get; set; }

        public double Probability { get; set; }

        public BoundingBox BoundingBox { get; set; }
    }
}
