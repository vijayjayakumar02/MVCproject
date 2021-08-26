using EmployeeDetails.Data;
using EmployeeDetails.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails.Controllers
{
    public class DeptController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DeptController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<DeptDetails> DepartmentList = _db.DepartmentInfo;

            return View(DepartmentList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DeptDetails objDept)
        {
            if (ModelState.IsValid)
            {
                _db.DepartmentInfo.Add(objDept);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(objDept);
        }
    }
}
