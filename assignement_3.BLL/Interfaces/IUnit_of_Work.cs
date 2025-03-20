using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignement_3.BLL.Interfaces
{
    public interface IUnit_of_Work :IDisposable
    {
        public IDepartmentReprositories DepartmentReprositories { get;  }
        public IEmployeeRespositry EmployeeRespositry { get;  }
        public int complete();

        

       
    }
}
