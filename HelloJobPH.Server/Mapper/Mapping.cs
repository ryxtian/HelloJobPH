using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Server.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<JobPosting, JobPostingDtos>().ReverseMap() ;
            CreateMap<HumanResources, HumanResourceDtos>().ReverseMap();
        }
    }
}
