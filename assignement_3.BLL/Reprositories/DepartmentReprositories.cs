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
    internal class DepartmentReprositories : IDepartmentReprositories
    {
        private CompanyDbContext context;
        public DepartmentReprositories()
        {
            context = new CompanyDbContext();
        }
        public IEnumerable<Department> GetAll()
        {
            return context.Departments.ToList();
                }

        public Department? GetDepartment(int id)
        {
            return context.Departments.Find(id);
        }

        public int Add(Department department)
        {
            context.Departments.Add(department);
            return context.SaveChanges();
        }

        public int Update(Department department)
        {
            context.Departments.Update(department);
            return context.SaveChanges();
        }

        public int Delete(Department department)
        {
            context.Departments.Remove(department);
            return context.SaveChanges();
        }

       

      
       
    }
}
