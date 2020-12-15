namespace GirlsHandmadeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductMaterial
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int MaterialId { get; set; }

        public virtual Material Material { get; set; }
    }
}
