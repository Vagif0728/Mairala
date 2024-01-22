using Mairala.Areas.Admin.ViewModels;

using Mairala.DAL;
using Mairala.Models;
using Mairala.Utilities.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Mairala.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees= await _context.Employees.ToListAsync();

            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM  employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            if (!employeeVM.Photo.FileType("image/"))
            {
                ModelState.AddModelError("Photo", "ghgfddd");
                return View();
            }

            if (!employeeVM.Photo.FileSize(3 * 1024))
            {
                ModelState.AddModelError("Photo", "dddgjhgd");
                return View();
            }

            string fileName = await employeeVM.Photo.CreateFileAsync(_env.WebRootPath,"assets","images");

            Employee employee = new Employee
            {
                Image = fileName,
                Name = employeeVM.Name,
                Department = employeeVM.Department,

            };

            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(int id)
        {

            if (id <= 0) return BadRequest();
            Employee employee = await _context.Employees.FirstOrDefaultAsync(e=> e.Id == id);
            if(employee == null) return NotFound();

            UpdateEmployeeVM employeeVM= new UpdateEmployeeVM
            {
                Name=employee.Name,
                Department=employee.Department,
                Image=employee.Image
               
            };

            return View(employeeVM);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateEmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Employee employee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);

            if (employee is null) return NotFound();

            if(employeeVM.Photo is not null)
            {
                if (!employeeVM.Photo.FileType("image/"))
                {
                    ModelState.AddModelError("Photo", "nijdgdgduy");
                    return View(employeeVM);
                }

                if (!employeeVM.Photo.FileSize(3 * 1024))
                {
                    ModelState.AddModelError("Photo", "djhsdghdfghd");
                    return View(employeeVM);
                }
                string newImage = await employeeVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");
                employee.Image.DeleteFileAsync(_env.WebRootPath, "assets", "images");
                employee.Image= newImage;
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> DeleteFile(int id)
        {
            if(id<=0) return BadRequest();
            Employee employee =  await _context.Employees.FirstOrDefaultAsync(e=> e.Id == id);
            if (employee == null) return NotFound();


            _context.Employees.Remove(employee);
            employee.Image.DeleteFileAsync(_env.WebRootPath, "assets", "images");
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
