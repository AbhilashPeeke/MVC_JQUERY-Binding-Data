using MVC_CRUD_jqry_Azax.Models;
using MVC_CRUD_jqry_Azax.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CRUD_jqry_Azax.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

        private AbhiEntities _db;
        public EmployeeController()
        {
            _db = new AbhiEntities();
        }
        public ActionResult Index()
        {
            ViewBag.Cities = (from obj in _db.Cities
                              select new SelectListItem()
                              {
                                  Text = obj.Name,
                                  Value = obj.CityId.ToString()
                              }).ToList();
            return View();
        }
        public JsonResult GetAllEmployee()
        {
            var GetEmpRecords = (from objEmp in _db.Employees
                                 join
                                 objCities in _db.Cities on
                                 objEmp.CityId equals objCities.CityId
                                 select new
                                 {
                                     EmployeeId = objEmp.EmployeeId,
                                     FirstName = objEmp.FirstName,
                                     LastName = objEmp.LastName,
                                     Depatment = objEmp.Department,
                                     JobType = objEmp.JobType,
                                     Salary = objEmp.Salary,
                                     CityId = objEmp.CityId,
                                     Name = objCities.Name
                                 }
                                 ).ToList();
            return Json(new { Success = true, data = GetEmpRecords},JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddUpdateEmployee(EmployeeViewModel objemployeeViewModel)
        {
            string Message = "Data Successfully Updated...!"; 
            if(!ModelState.IsValid)
            {
                var ErrorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();
                return Json(new { Success = false, Message = "Some problem in validation...", ErrorList = ErrorList });
            }

            Employee objemployee = _db.Employees.
                                    SingleOrDefault(model => model.EmployeeId == objemployeeViewModel.EmployeeId) ?? new Employee();
            objemployee.EmployeeId = objemployeeViewModel.EmployeeId;
            objemployee.FirstName = objemployeeViewModel.FirstName;
            objemployee.LastName = objemployeeViewModel.LastName;
            objemployee.Department = objemployeeViewModel.Department;
            objemployee.JobType = objemployeeViewModel.JobType;
            objemployee.Salary = objemployeeViewModel.Salary;
            objemployee.CityId = objemployeeViewModel.CityId;
            
            if(objemployeeViewModel.EmployeeId == 0)
            {
                Message = "Data Successfully Added...!";
                _db.Employees.Add(objemployee);
            }
            _db.SaveChanges();
            return Json(new {Success = true, Message = Message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditEmployee(int employeeId)
        {
            return Json(_db.Employees.SingleOrDefault(model => model.EmployeeId == employeeId),
                        JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteEmployee(int employeeId)
        {
            Employee objemployee = _db.Employees.Single(model => model.EmployeeId == employeeId);
            _db.Employees.Remove(objemployee);
            _db.SaveChanges();
            return Json(new { Success = true, Message = "Data Successfully Deleted...!" }, JsonRequestBehavior.AllowGet);
        }
    }
}