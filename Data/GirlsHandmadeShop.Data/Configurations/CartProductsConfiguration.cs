namespace GirlsHandmadeShop.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GirlsHandmadeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CartProductsConfiguration : IEntityTypeConfiguration<CartProducts>
    {
        public void Configure(EntityTypeBuilder<CartProducts> builder)
        {
            builder
                .HasKey(x => new { x.ProductId, x.UserId });
        }
    }
}
