using assignement_3.BLL.Interfaces;
using assignement_3.BLL.Reprositories;
using Microsoft.AspNetCore.Mvc;

namespace assignement_3.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentReprositories _departmentReprositories;
       public DepartmentController(IDepartmentReprositories departmentReprositories) {
        
        _departmentReprositories = departmentReprositories;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var department = _departmentReprositories.GetAll();
            return View(department);
        }
    }
}
