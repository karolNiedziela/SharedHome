﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.EF.Configurations.Read
{
    internal class ShoppingListProductReadConfiguration : IEntityTypeConfiguration<ShoppingListProductReadModel>
    {
        public void Configure(EntityTypeBuilder<ShoppingListProductReadModel> builder)
        {
            builder.ToTable("ShoppingListProducts");

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Id)
                .HasColumnName("ShoppingListProductId");
        }
    }
}
