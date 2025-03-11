using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignement_3.BLL.Interfaces;
using assignement_3.DAL.Data.contexts;
using assignement_3.DAL.Models;

namespace assignement_3.BLL.Reprositories
{
    public class EmployeeResporitory : GeneralResporitory<Employee>, IEmployeeRespositry
    {
        public EmployeeResporitory(CompanyDbContext _context) : base(_context)
        {
        }
    }
}
