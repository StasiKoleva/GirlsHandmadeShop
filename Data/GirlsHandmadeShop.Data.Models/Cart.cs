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
            this.CartProducts = new HashSet<CartProducts>();
        }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<CartProducts> CartProducts { get; set; }
    }
}
