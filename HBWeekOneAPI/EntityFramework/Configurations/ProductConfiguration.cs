using HBWeekOneAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBWeekOneAPI.EntityFramework.Configurations
{
    public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}