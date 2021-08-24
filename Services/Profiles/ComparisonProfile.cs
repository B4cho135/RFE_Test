using AutoMapper;
using Core.Entities;
using Models.Comparisons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class ComparisonProfile : Profile
    {
        public ComparisonProfile()
        {
            CreateMap<ComparisonEntity, ComparisonModel>();
            CreateMap<ComparisonModel, ComparisonEntity>();
        }
    }
}
