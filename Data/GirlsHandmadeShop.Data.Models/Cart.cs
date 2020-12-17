namespace GirlsHandmadeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using GirlsHandmadeShop.Data.Common.Models;

    public class Cart : BaseDeletableModel<string>
    {
        public Cart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new HashSet<ProductCart>();
        }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public virtual ICollection<ProductCart> Products { get; set; }
    }
}
