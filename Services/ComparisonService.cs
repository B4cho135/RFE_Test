using AutoMapper;
using Core.Entities;
using Core.Persistance;
using Models.Comparisons;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ComparisonService : GenericService<ComparisonEntity, ComparisonModel>, IComparisonService
    {
        public ComparisonService(DefaultDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public ComparisonModel Compare(string right, string left)
        {
            if(right.Equals(left))
            {
                return new ComparisonModel() {
                    Result = "inputs were equal"
                };
            }

            if(right.Length != left.Length)
            {
                return new ComparisonModel()
                {
                    Result = "inputs are of different size"
                };
            }

            if(right.Length == left.Length && right != left)
            {


                string tmp = "";
                string tmp2 = "";
                var offsets = new List<string>();

                //for an extra iteration, since it skips last character the way it is implemented now
                left = left + " ";
                right = right + " ";

                for (int i = 0; i < left.Length; i++)
                {
                    if (left[i] == right[i])
                    {
                        if (!string.IsNullOrEmpty(tmp))
                        {
                            offsets.Add(tmp);
                            offsets.Add(tmp2);
                        }
                        tmp = "";
                        tmp2 = "";
                        continue;
                    }

                    tmp = tmp + left[i];
                    tmp2 = tmp2 + right[i];

                }

                var comparison = new ComparisonModel();

                foreach (var offset in offsets)
                {
                    comparison.Offsets.Add(new Offset()
                    {
                        Difference = offset,
                        Size = offset.Length
                    });
                }
                return comparison;
            }

            return new ComparisonModel()
            {
                Result = "Something went wrong"
            };
        }
    }
}
