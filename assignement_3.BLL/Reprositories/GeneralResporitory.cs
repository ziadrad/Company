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
        public async Task< IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await  context.Employees.Include(E => E.Department).ToListAsync();
            }
          
            return (IEnumerable<T>) await context.Set<T>().ToListAsync();
        }

        public async Task<T>? Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E => E.Id == id) as T ;
            }
            return  await context.Set<T>().FindAsync(id) ;
        }

        public async Task<IEnumerable<T>>? GetByName(string name)
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            }
            return  await context.Set<T>().Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task AddAsync(T department)
        {
           await context.Set<T>().AddAsync(department);
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
