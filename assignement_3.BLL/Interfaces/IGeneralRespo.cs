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
        T? Get(int id);
        int Add(T model);
        int Update(T model);
        int Delete(T model);
    }
}
