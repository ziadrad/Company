using assignement_3.BLL.Interfaces;
using assignement_3.BLL.Reprositories;
using assignement_3.DAL.Models;
using assignement_3.PL.dto;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                };
                var count = _departmentReprositories.Add(department);
                if (count > 0) {
                    return RedirectToAction(nameof(Index));
                };
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details( Department model)
        {
            var department = _departmentReprositories.GetDepartment(model.Id);
            return View(department);
        }
    }
}
