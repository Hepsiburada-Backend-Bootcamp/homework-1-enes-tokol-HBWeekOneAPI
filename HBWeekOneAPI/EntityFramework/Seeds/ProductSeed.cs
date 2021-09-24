using HBWeekOneAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBWeekOneAPI.EntityFramework.Seeds
{
    public class ProductSeed:IEntityTypeConfiguration<Product>
    {
        public ProductSeed()
        {
            
        }
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1,Name = "Sony Xperia L1"},
                new Product { Id = 2,Name = "Huawei Mate 9 "},
                new Product { Id = 3,Name = "Iphone 13"},
                new Product { Id = 4,Name = "Redmi Note 10"});
        }
    }
}