using FCoreCrudTest.DB_Folder;
using FCoreCrudTest.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FCoreCrudTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LogInModel uobj)
        {
            FCrudContext dbobj = new FCrudContext();
            var res = dbobj.LoginTables.Where(a => a.Email == uobj.Email).FirstOrDefault();
            if (res == null)
            {
                TempData["Invalid"] = "Email not found";
            }
            else
            {
                if (res.Email == uobj.Email && res.Password == uobj.Password)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, res.Name), new Claim(ClaimTypes.Email, res.Email) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);
                
                    return RedirectToAction("UserForm", "Home");
                }
                else
                {
                    ViewBag.Inv = "Wrong Email Or Password";
                    return View();
                }
            }

            return View();
          
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View("Index");
        }






        [Authorize]

        public IActionResult UserTable()
        {
            FCrudContext dbobj = new FCrudContext();
            var res = dbobj.FuserTables.ToList();
            List<User_Class> mobj = new List<User_Class>();
            foreach (var item in res)
            {
                mobj.Add(new User_Class
                {
                    Id = item.Id,
                    Name = item.Name,
                    Age = item.Age,
                    Technology = item.Technology,
                    Email = item.Email
                });

            }
            return View(mobj);
           
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Authorize]
        [HttpGet]
        public IActionResult UserForm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult UserForm(User_Class mobj)
        {
            FCrudContext dbobj = new FCrudContext();

            FuserTable tobj = new FuserTable();

            tobj.Id = mobj.Id;
            tobj.Name = mobj.Name;
            tobj.Age = mobj.Age;
            tobj.Technology = mobj.Technology;
            tobj.Email = mobj.Email;
           
            if (mobj.Id == 0)
            {
                dbobj.FuserTables.Add(tobj);
                dbobj.SaveChanges();
            }
            else
            {
                dbobj.Entry(tobj).State = EntityState.Modified;
                dbobj.SaveChanges();


            }
            return RedirectToAction("UserTable");
        }


        [Authorize]
        public IActionResult Delete(int Id)
        {
            FCrudContext dbobj = new FCrudContext();
            var d_item = dbobj.FuserTables.Where(m => m.Id == Id).First();
            dbobj.FuserTables.Remove(d_item);
            dbobj.SaveChanges();
            return RedirectToAction("UserTable");
        }


        [Authorize]
        public IActionResult Edit(int Id)
        {
            User_Class mobj = new User_Class();
            FCrudContext dbobj = new FCrudContext();

            var eobj = dbobj.FuserTables.Where(m => m.Id == Id).First();
            mobj.Id = eobj.Id;
            mobj.Name = eobj.Name;
            mobj.Age = eobj.Age;
            mobj.Technology = eobj.Technology;
            mobj.Email = eobj.Email;
          

            return View("UserForm", mobj);
        }
      

    }
}
