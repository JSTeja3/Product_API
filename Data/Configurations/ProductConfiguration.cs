using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product_API.Models;

namespace Product_API.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProductId)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => new { p.ProductId, p.Version })
                .IsUnique();    

            builder.Property(p => p.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.Property(p => p.Version)
                .IsRequired();

            builder.Property(p => p.State)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .IsRequired();
        }
    }
}