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
        IEnumerable<T> GetAll();
        List<T> GetByName(string SearchInput);
        T? Get(int id);
        void Add(T model);
        void Update(T model);
        void Delete(T model);
    }
}
