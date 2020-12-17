namespace GirlsHandmadeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductCart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int CartId { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
