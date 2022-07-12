using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_ecommerce_db
{
    [Table("customers")]
    [Index (nameof(Email), IsUnique=true)]
    public class Customers{
        [Key]
        public int CustomersId { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        List<Orders> Orders { get; set; }
    }
}
