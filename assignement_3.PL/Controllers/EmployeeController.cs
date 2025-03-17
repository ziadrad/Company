using assignement_3.BLL.Interfaces;
using assignement_3.DAL.Models;
using assignement_3.PL.dto;
using Microsoft.AspNetCore.Mvc;

namespace assignement_3.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRespositry _employeeRespositry;
        public EmployeeController(IEmployeeRespositry employeeRespositry)
        {

            _employeeRespositry = employeeRespositry;
        }



        [HttpGet]
        public IActionResult Index()
        {
            var employee = _employeeRespositry.GetAll();
            return View(employee);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Create(CreatEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {

                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    Phone = model.Phone,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                };
                var count = _employeeRespositry.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "new Employee is created";
                    return RedirectToAction(nameof(Index));
                };
            }
            return View();
        }



        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest(error: "Invalid Id"); // 400

            var employee = _employeeRespositry.Get(id.Value);
            if (employee is null) return NotFound(new
            {
                statusCode = 404,
                message = $"Employee With Id :{id} is not found",
            }
            );
            return View(viewName, employee);
        }




        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id is null) return BadRequest(error: "Invalid Id"); // 400

            var employee = _employeeRespositry.Get(id.Value);
            if (employee is null) return NotFound(new
            {
                statusCode = 404,
                message = $"Employee With Id :{id} is not found",
            }
            );
            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee model)
        {

                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    Phone = model.Phone,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                };
                var count = _employeeRespositry.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                };
            
            return View();
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    Phone = model.Phone,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                };
                var count = _employeeRespositry.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                };
            }
            return View();
        }
    }
}
