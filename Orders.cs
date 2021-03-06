using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_ecommerce_db
{
    [Table("orders")]
    public class Orders{
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public double Amount { get; set; }
        public string Status { get; set; }


        public int CustomersId { get; set; }
        public Customers Customer { get; set; }

        List<Products> Products { get; set; }

        public Orders(DateTime date, double amount, string status, Customers customer, List<Products> products){

            Date = date;
            Amount = amount;
            Status = status;
            Customer = customer;
            Products = products;
        }

        public Orders()
        {

        }
    }
}
