using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EfCore.Mapping
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Name).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.Picture).HasMaxLength(1000).IsRequired(false);
            builder.Property(c => c.PictureAlt).HasMaxLength(255);
            builder.Property(c => c.PictureTitle).HasMaxLength(500);
            builder.Property(c => c.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(c => c.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(c => c.Slug).HasMaxLength(300).IsRequired();
            builder.Property(c => c.CanonicalAddress).HasMaxLength(1000).IsRequired(false);
        }
    }
}
