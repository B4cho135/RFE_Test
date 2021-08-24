using Core.Entities;
using Models.Comparisons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IComparisonService : IGenericService<ComparisonEntity, ComparisonModel>
    {
        ComparisonModel Compare(string right, string left);
    }
}
