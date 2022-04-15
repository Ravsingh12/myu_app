using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.DB_Context;
using MyApp.Models;
using myu_app.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace myu_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public long Mobile { get; private set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<empmodel> mod = new List<empmodel>();
            CrudContext obj = new CrudContext();
            var res = obj.EmpDetails.ToList();
            foreach(var item in res)
            {
                mod.Add(new empmodel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Mobile = item.Mobile,
                    Department = item.Department,
                    City = item.City 
                });
            }
            return View(res);

            
        }
        [HttpGet]
        public IActionResult add_emp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult add_emp(empmodel mod)
        {
            CrudContext ent = new CrudContext();
            EmpDetail tb = new EmpDetail();
            tb.Id = mod.Id;
            tb.Name = mod.Name;
            tb.Email = mod.Email;
            tb.Department = mod.Department;
            tb.Mobile = mod.Mobile;
            tb.City = mod.City;
            if (mod.Id == 0)
            {
                ent.EmpDetails.Add(tb);
                ent.SaveChanges();
            }
            else
            {
                ent.Entry(tb).State = EntityState.Modified;
                ent.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        public IActionResult edit( int id)
        {
            CrudContext ent = new CrudContext();
            var edit = ent.EmpDetails.Where(m => m.Id == id).First();

            empmodel mod = new empmodel();

            mod.Id = edit.Id;
            mod.Name = edit.Name;
            mod.Email = edit.Email;
            mod.Department = edit.Department;
            mod.Mobile = edit.Mobile;
            mod.City = edit.City;
            return View("add_emp", mod);
        }
        public IActionResult delete( int id)
        {
            CrudContext ent = new CrudContext();
            var dlt = ent.EmpDetails.Where(m => m.Id == id).First();
            ent.EmpDetails.Remove(dlt);
            ent.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Admin_login(int ad)
        {
            CrudContext ent = new CrudContext();
            var adm = ent.EmpDetails.Where(m => m.Mobile == Mobile).First();
            empmodel lg = new empmodel();
            lg.Email = adm.Email;
            lg.Mobile = adm.Mobile;
            return View("Adm_login", lg);
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
