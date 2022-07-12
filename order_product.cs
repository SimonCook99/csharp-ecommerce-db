using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_ecommerce_db
{
    public class order_product{

        [Key]
        public int order_productId { get; set; }

        public int OrdersId { get; set; }
        public Orders Orders { get; set; }


        public int ProductsId { get; set; }
        public Products Products { get; set; }

        public int Quantity { get; set; }
    }
}
