using Microsoft.AspNetCore.Mvc;
using practice1.Models;
using System.Diagnostics;

namespace practice1.Controllers
{
    public class HomeController : Controller
    {

        private readonly EmployeDbContext context;

        public HomeController(EmployeDbContext context)
        {
           this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            List<Employee> employees = context.Employees.ToList();
            return View(employees);
        }

        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                Employee emp = context.Employees.FirstOrDefault(item=>item.EmpId==id);

                if(emp != null)
                {
                    TempData["massage"] = "Record found Succefully";
                    return View(emp);
                }
                else
                {
                    TempData["massage"] = "Record not found for roll:" + id;
                    return RedirectToAction("List");
                }
            }
            else
            {
                TempData["massage"] = "please enter valid id";
                return RedirectToAction("List");

            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee em)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    context.Employees.Add(em);
                    context.SaveChanges();
                    TempData["mass"] = "Record SuccessFully Added";
                }
                catch (Exception )
                {
                    TempData["massage"] = "Enable to Data Inserting, Please Check:";
                }
                return RedirectToAction("List");
            }
            return View(em);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
              Employee em = context.Employees.FirstOrDefault(item => item.EmpId == id);
                if (em != null)
                {
                    return View(em);
                }
                else
                {
                    TempData["massage"] = "Unable to Find record  for roll:" + id;
                    return RedirectToAction("List");
                }



            }
            TempData["massage"] = "Please pass Roll number for Edit Data..";
            return RedirectToAction("List");


        }

        [HttpPost]
        public IActionResult Edit(Employee em)
        {
            if (ModelState.IsValid)
            {
                context.Employees.Update(em);
                context.SaveChanges();
                TempData["mass"] = "Record SuccessFully updated";
                return RedirectToAction("List");
            }
            return View(em);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Employee em = context.Employees.FirstOrDefault(item => item.EmpId == id);
                if (em != null)
                {
                    return View(em);
                }
                TempData["massage"] = "Unable to Find record  for roll:" + id;
                return RedirectToAction("List");

            }
            TempData["massage"] = "Please pass Roll number for Delete Data..";
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(Employee em)
        {
            if (em != null)
            {
                context.Employees.Remove(em);
                context.SaveChanges();
                TempData["mass"] = "Record SuccessFully Deleted";
                return RedirectToAction("List");
            }
            TempData["massage"] = "Please pass valid Roll number for Delete Data..";
            return RedirectToAction("List");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
