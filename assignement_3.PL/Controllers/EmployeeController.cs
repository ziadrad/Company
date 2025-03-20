using assignement_3.BLL.Interfaces;
using assignement_3.BLL.Reprositories;
using assignement_3.DAL.Models;
using assignement_3.PL.dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace assignement_3.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnit_of_Work _unit_Of_Work;
        private readonly IMapper _mapper;

        public EmployeeController(IUnit_of_Work unit_Of_Work, IMapper mapper)
        {

            _unit_Of_Work = unit_Of_Work;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult Index( string? SearchInput)
        {
            IEnumerable<Employee> employee;
            if (SearchInput == null)
            {
                 employee = _unit_Of_Work.EmployeeRespositry.GetAll();
            }
            else
            {
                 employee = _unit_Of_Work.EmployeeRespositry.GetByName(SearchInput);
            }
           
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
                //var employee = new Employee()
                //{

                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    Phone = model.Phone,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    Email = model.Email,
                //   Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DepartmentId = model.DepartmentId
                //};
              var employee =   _mapper.Map<Employee>(model);
                _unit_Of_Work.EmployeeRespositry.Add(employee);
                var count = _unit_Of_Work.complete();

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

            var employee = _unit_Of_Work.EmployeeRespositry.Get(id.Value);
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

            var employee = _unit_Of_Work.EmployeeRespositry.Get(id.Value);
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

            //var employee = new Employee()
            //{
            //    Id = id,
            //    Name = model.Name,
            //    Address = model.Address,
            //    Age = model.Age,
            //    Phone = model.Phone,
            //    CreateAt = model.CreateAt,
            //    HiringDate = model.HiringDate,
            //    Email = model.Email,
            //    IsActive = model.IsActive,
            //    IsDeleted = model.IsDeleted,
            //};
            var employee = _mapper.Map<Employee>(model);

            _unit_Of_Work.EmployeeRespositry.Delete(employee);
            var count = _unit_Of_Work.complete();

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
                //var employee = new Employee()
                //{
                //    Id = id,
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    Email = model.Email,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DepartmentId = model.DepartmentId
                //};
                var employee = _mapper.Map<Employee>(model);

              _unit_Of_Work.EmployeeRespositry.Update(employee);
                var count = _unit_Of_Work.complete();

                if (count > 0)
                {
                    TempData["Message"] = "new Employee is edited";
                    return RedirectToAction(nameof(Index));
                };
            }
            return View();
        }
    }
}
