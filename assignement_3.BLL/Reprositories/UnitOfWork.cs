using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignement_3.BLL.Interfaces;
using assignement_3.DAL.Data.contexts;

namespace assignement_3.BLL.Reprositories
{
    public class UnitOfWork : IUnit_of_Work 
    {
        public IDepartmentReprositories DepartmentReprositories {  get; }

        public IEmployeeRespositry EmployeeRespositry {  get; }
        public CompanyDbContext _context { get; }

        public UnitOfWork(CompanyDbContext context)
        {
            
        _context = context;
            DepartmentReprositories =new DepartmentReprositories(_context);
            EmployeeRespositry = new EmployeeResporitory(_context);

        }

        public async Task<int> completeAsync() => await _context.SaveChangesAsync();


       
        public ValueTask DisposeAsync()
        {
           return  _context.DisposeAsync();
        }
    }
}
