using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastracture.EFCore.Mapping
{
    internal class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IssueTrackingNo).HasMaxLength(8).IsRequired(false);

            builder.OwnsMany(x => x.Items, navigationbuilder => {
                navigationbuilder.ToTable("OrderItems");
                navigationbuilder.HasKey(x => x.Id);
                navigationbuilder.WithOwner(x => x.Order).HasForeignKey(x => x.OrderId);
            });
        }
    }
}
