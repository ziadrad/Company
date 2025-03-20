using assignement_3.DAL.Models;
using assignement_3.PL.dto;
using AutoMapper;

namespace assignement_3.PL.Mapping
{
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreatEmployeeDto, Employee>();
            CreateMap<Employee, CreatEmployeeDto>();
        }
    }
}
