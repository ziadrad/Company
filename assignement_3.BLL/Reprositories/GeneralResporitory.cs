using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignement_3.BLL.Interfaces;
using assignement_3.DAL.Data.contexts;
using assignement_3.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace assignement_3.BLL.Reprositories
{
    public class GeneralResporitory<T> : IGeneralRespo<T> where T : BaseEntity
    {
        private readonly CompanyDbContext context;

        public GeneralResporitory(CompanyDbContext _context)
        {

            context = _context;
        }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)context.Employees.Include(E => E.Department).ToList();
            }
          
            return context.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return context.Employees.Include(E => E.Department).FirstOrDefault(E=>E.Id == id) as T;
            }
            return context.Set<T>().Find(id);
        }

        public List<T>? GetByName(string name)
        {
            if (typeof(T) == typeof(Employee))
            {
                return context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList() as List<T>;
            }
            return context.Set<T>().Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public void Add(T department)
        {
            context.Set<T>().Add(department);
        }

        public void Update(T department)
        {
            context.Set<T>().Update(department);
        }

        public void Delete(T department)
        {
            context.Set<T>().Remove(department);
        }

     
    }
}
