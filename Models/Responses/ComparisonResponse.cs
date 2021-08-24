using Models.Comparisons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class ComparisonResponse
    {
        public string Message { get; set; }
        public List<Offset> Offsets { get; set; } = new List<Offset>();
        public bool IsSucceeded { get; set; }
    }
}
