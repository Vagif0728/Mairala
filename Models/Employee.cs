using System.ComponentModel.DataAnnotations.Schema;

namespace Mairala.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }

        public string Image { get; set; }
        
    }
}
