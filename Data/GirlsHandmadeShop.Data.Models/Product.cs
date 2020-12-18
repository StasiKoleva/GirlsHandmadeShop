namespace GirlsHandmadeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GirlsHandmadeShop.Data.Common.Models;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Materials = new HashSet<ProductMaterial>();
            this.Images = new HashSet<Image>();
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
            this.CartProducts = new HashSet<CartProducts>();
        }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Availability { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<ProductMaterial> Materials { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<CartProducts> CartProducts { get; set; }
    }
}
