using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comparisons
{
    public class ComparisonModel
    {
        public Guid ID { get; set; }
        public string RightSide { get; set; }
        public string LeftSide { get; set; }
        public string Result { get; set; }
        public List<Offset> Offsets { get; set; } = new List<Offset>();
    }
}
