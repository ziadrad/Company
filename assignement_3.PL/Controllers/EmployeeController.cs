using assignement_3.BLL.Interfaces;
using assignement_3.BLL.Reprositories;
using assignement_3.DAL.Models;
using assignement_3.PL.dto;
using assignement_3.PL.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace assignement_3.PL.Controllers
{
    [Authorize]
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
        public async Task <IActionResult> Index( )
        {
            IEnumerable<Employee> employee;
                 employee = await _unit_Of_Work.EmployeeRespositry.GetAll();

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? SearchInput)
        {
            IEnumerable<Employee> employee;
            if (SearchInput == null)
            {
                employee = await _unit_Of_Work.EmployeeRespositry.GetAll();
            }
            else
            {
                employee = await _unit_Of_Work.EmployeeRespositry.GetByName(SearchInput);
            }

            return PartialView("_EmployeeTablePartialView", employee);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task <IActionResult> Create(CreatEmployeeDto model)
        {

            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                {
                  model.ImageName=  DocumentSetting.UploadFile(model.Image,"images");
                }
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
                await _unit_Of_Work.EmployeeRespositry.AddAsync(employee);
                var count = await _unit_Of_Work.completeAsync();

                if (count > 0)
                {
                    TempData["Message"] = "new Employee is created";
                    return RedirectToAction(nameof(Index));
                };
            }
            return View();
        }



        [HttpGet]
        public async Task <IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest(error: "Invalid Id"); // 400
            dynamic employee;
            if (viewName == "Details")
            {
               employee = _mapper.Map<Employee>(await _unit_Of_Work.EmployeeRespositry.Get(id.Value));

            }
            else
            {
                employee = _mapper.Map<CreatEmployeeDto>(await _unit_Of_Work.EmployeeRespositry.Get(id.Value));

            }

            if (employee is null) return NotFound(new
            {
                statusCode = 404,
                message = $"Employee With Id :{id} is not found",
            }
            );
           
            return View(viewName, employee);
        }




        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null) return BadRequest(error: "Invalid Id"); // 400

            var employee = await _unit_Of_Work.EmployeeRespositry.Get(id.Value);
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
        public async Task<IActionResult> Delete([FromRoute] int id, Employee model)
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
            var count = await _unit_Of_Work.completeAsync();

            if (count > 0)
                {
                  if (model.ImageName is not null)
                    {
                    try
                    {
                        DocumentSetting.DeletFile(model.ImageName, "images");
                    }
                    catch (Exception e)
                    {
                        BadRequest(error: "Invalid");
                    }
                    }
                    return RedirectToAction(nameof(Index));
                };
            
            return View();
        }


        [HttpGet]
        public Task<IActionResult> Edit(int? id)
        {
            return Details(id, "Edit");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreatEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");

                    if (model.ImageName is not null)
                    {
                        DocumentSetting.DeletFile(model.ImageName, "images");
                    }

                }
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
             employee.Id = id;
              _unit_Of_Work.EmployeeRespositry.Update(employee);
                var count = await _unit_Of_Work.completeAsync();
              
               
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
