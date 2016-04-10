using RepositoryPatternMVCWithEntityFramework.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//分页引用
using PagedList;
namespace RepositoryPatternMVCWithEntityFramework.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository<Employee> employeeRepository;
        public EmployeeController()
        {
            this.employeeRepository = new EmployeeRepository(new EmployeeManagementDBEntities());

        }

        // GET: Employee
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "ID" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var employees = from s in employeeRepository.GetAllEmployee()
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                || s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "ID":
                    employees = employees.OrderByDescending(s => s.ID);
                    break;
                case "Name":
                    employees = employees.OrderBy(s => s.Name);
                    break;
                default:
                    employees = employees.OrderBy(s => s.ID);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Employee/Details/5    

        public ViewResult Details(int id)
        {
            Employee emp = employeeRepository.GetEmployeeById(id);
            return View(emp);
        }

        // GET: /Employee/Create    

        public ActionResult Create()
        {
            return View();
        }

        // POST: /Employee/Create    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
        [Bind(Include = "Name, Email,Age")] Employee emp)   //备注，Employee中还有一个Age，如果没有Include到，那么到时候添加的时候，Age就一直是0
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeRepository.InsertEmployee(emp);
                    employeeRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Some Error Occured.");
            }
            return View(emp);
        }

        // GET: /Employee/Edit/5    

        public ActionResult Edit(int id)
        {
            Employee emp = employeeRepository.GetEmployeeById(id);
            return View(emp);
        }

        // POST: /Employee/Edit/5    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeRepository.UpdateEmployee(emp);
                    employeeRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Some error Occured.");
            }
            return View(emp);
        }

        // GET: /employee/Delete/5    

        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Some Error Occured.";
            }
            Employee emp = employeeRepository.GetEmployeeById(id);
            return View(emp);
        }

        // POST: /Employee/Delete/5    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Employee emp = employeeRepository.GetEmployeeById(id);
                employeeRepository.DeleteEmployee(id);
                employeeRepository.Save();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            employeeRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}