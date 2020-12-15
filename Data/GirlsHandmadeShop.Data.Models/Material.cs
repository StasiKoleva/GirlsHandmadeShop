namespace GirlsHandmadeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GirlsHandmadeShop.Data.Common.Models;

    public class Material : BaseDeletableModel<int>
    {
        public Material()
        {
            this.Products = new HashSet<ProductMaterial>();
        }

        public string Name { get; set; }

        public ICollection<ProductMaterial> Products { get; set; }
    }
}
