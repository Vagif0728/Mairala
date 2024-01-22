using System.ComponentModel.DataAnnotations;

namespace Mairala.Areas.Admin.ViewModels
{
    public class CreateEmployeeVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public IFormFile Photo { get; set; }

    }
}
