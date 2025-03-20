using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignement_3.DAL.Models;

namespace assignement_3.BLL.Interfaces
{
    public interface IGeneralRespo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task< IEnumerable<T>> GetByName(string SearchInput);
        Task<T>? Get(int id);
        Task AddAsync(T model);
        void Update(T model);
        void Delete(T model);
    }
}
