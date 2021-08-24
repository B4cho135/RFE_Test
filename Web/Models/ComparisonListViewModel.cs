using Models.Comparisons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ComparisonListViewModel
    {
        public Guid? ID { get; set; }
        public byte[] RightSide { get; set; }
        public byte[] LeftSide { get; set; }
        public List<ComparisonModel> Comparisons { get; set; }
    }
}
