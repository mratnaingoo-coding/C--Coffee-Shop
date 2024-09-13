using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCoffeeConsoleApp.Databases;
[Table("tbl_user")]
public class CSUserDto
{
    [Key]
    public int userId { get; set; }
    public string? userName { get; set; }
    public string? password { get; set; }
    public string? userRole { get; set; }
    public bool isAdmin { get; set; }
}
