using assignement_3.BLL.Interfaces;
using assignement_3.BLL.Reprositories;
using assignement_3.DAL.Models;
using assignement_3.PL.dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace assignement_3.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnit_of_Work unit_Of_Work;
        private readonly IMapper mapper;

        public DepartmentController(IUnit_of_Work unit_Of_Work, IMapper mapper)
        {
        
            this.unit_Of_Work = unit_Of_Work;
            this.mapper = mapper;
        }



        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Department> department;
            if (SearchInput == null)
            {
                department = unit_Of_Work.DepartmentReprositories.GetAll();
            }
            else
            {
                department = unit_Of_Work.DepartmentReprositories.GetByName(SearchInput);
            }

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
                //var department = new Department()
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreateAt,
                //};
                var department = mapper.Map<Department>(model);
               unit_Of_Work.DepartmentReprositories.Add(department);
                var count = unit_Of_Work.complete();
                if (count > 0) {
                    TempData["Message"] = "new Department is created";

                    return RedirectToAction(nameof(Index));
                };
            }
            return View();
        }



        [HttpGet]
        public IActionResult Details( int? id , string viewName= "Details")
        {
            if (id is null) return BadRequest(error: "Invalid Id"); // 400

            var department = unit_Of_Work.DepartmentReprositories.Get(id.Value);
            if (department is null) return NotFound(new
            {
                statusCode = 404,
                message = $"Department With Id :{id} is not found",
            }
            );
            return View(viewName,department);
        }
        



        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id is null) return BadRequest(error: "Invalid Id"); // 400

            var department = unit_Of_Work.DepartmentReprositories.Get(id.Value);
            if (department is null) return NotFound(new
            {
                statusCode = 404,
                message = $"Department With Id :{id} is not found",
            }
            );
            return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id,Department model)
        {

            if (ModelState.IsValid)
            {
                //var department = new Department()
                //{
                //    Id = id,
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreateAt,
                //};
                var department = mapper.Map<Department>(model);
                unit_Of_Work.DepartmentReprositories.Delete(department);
                var count = unit_Of_Work.complete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                };
            }
            return View();
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id,"Edit");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department model)
        {
            if (ModelState.IsValid)
            {
                //var department = new Department()
                //{
                //    Id = id,
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreateAt,
                //};
                var department = mapper.Map<Department>(model);
                 unit_Of_Work.DepartmentReprositories.Update(department);
                var count = unit_Of_Work.complete();
    
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                };
            }
            return View();
        }
    }
}
