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
    public class GeneralResporitory<T> : IGeneralRespo<T> where T : BaseEntity
    {
        private readonly CompanyDbContext context;

        public GeneralResporitory(CompanyDbContext _context)
        {

            context = _context;
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public int Add(T department)
        {
            context.Set<T>().Add(department);
            return context.SaveChanges();
        }

        public int Update(T department)
        {
            context.Set<T>().Update(department);
            return context.SaveChanges();
        }

        public int Delete(T department)
        {
            context.Set<T>().Remove(department);
            return context.SaveChanges();
        }


    }
}
