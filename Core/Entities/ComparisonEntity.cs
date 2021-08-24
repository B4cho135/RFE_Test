using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ComparisonEntity : BaseEntity<Guid>
    {
        public string RightSide { get; set; }
        public string LeftSide { get; set; }
    }
}
