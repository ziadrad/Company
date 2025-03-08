using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignement_3.DAL.Models;

namespace assignement_3.BLL.Interfaces
{
    public interface IDepartmentReprositories
    {
        IEnumerable<Department> GetAll();
        Department? GetDepartment(int id);
        int Add(Department department);
        int Update(Department department);
        int Delete(Department department);
    }
}
