using assignement_3.DAL.Models;
using assignement_3.PL.dto;
using AutoMapper;

namespace assignement_3.PL.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreatDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();
        }
    }
}
