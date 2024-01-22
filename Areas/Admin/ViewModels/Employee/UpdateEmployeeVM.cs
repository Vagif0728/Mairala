using System.ComponentModel.DataAnnotations;

namespace Mairala.Areas.Admin.ViewModels;

public class UpdateEmployeeVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Department { get; set; }
    public string? Image { get; set; }
    public IFormFile? Photo { get; set; }
}
