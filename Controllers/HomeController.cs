using Mairala.DAL;
using Mairala.Models;
using Mairala.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Mairala.Contrellers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           List<Employee> employees = _context.Employees.ToList();
            HomeVM home = new HomeVM 
            { 
                Employees = employees
            };


            return View(home);
        }
    }
}
