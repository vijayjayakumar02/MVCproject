using EmployeeDetails.Data;
using EmployeeDetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails.Controllers
{
    public class EmpController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmpController(ApplicationDbContext db)
        {
            _db = db;

        }    
        public IActionResult Index()
        {
            var EmployeeList = from item in _db.EmployeeInfo
                               where item.Status == true
                               select item;
            return View(EmployeeList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> MyDept = _db.DepartmentInfo.Select(x =>
                new SelectListItem
                {
                    Text = x.Id.ToString(),
                    Value = x.Id.ToString()
                });

            ViewBag.myDropdown = MyDept;
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmpDetails objEmp)
        {
            if (ModelState.IsValid)
            {
                _db.EmployeeInfo.Add(objEmp);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(objEmp);
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objEmp = _db.EmployeeInfo.FirstOrDefault(x => x.Id == id);
            if (objEmp == null)
            {
                return NotFound();
            }

            return View(objEmp);
        }
        [HttpPost]
        public IActionResult Update(EmpDetails objEmp)
        {
            if (ModelState.IsValid)
            {
                _db.EmployeeInfo.Update(objEmp);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objEmp);

        }
        public IActionResult Delete(int? id)
        {
            var objEmp = _db.EmployeeInfo.Find(id);
            if (objEmp == null)
            {
                return NotFound();
            }

            _db.EmployeeInfo.Remove(objEmp);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Status(int? id)
        {
            var objEmp = _db.EmployeeInfo.Find(id);
            var Empstatus = new EmpDetails
            {
                Id = objEmp.Id,
                Name = objEmp.Name,
                Designation = objEmp.Designation,
                DepartmentId = objEmp.DepartmentId,
                Status = !objEmp.Status
            };

            if (ModelState.IsValid)
            {
                _db.EmployeeInfo.Remove(objEmp);
                _db.EmployeeInfo.Update(Empstatus);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objEmp);

        }
    }
}
