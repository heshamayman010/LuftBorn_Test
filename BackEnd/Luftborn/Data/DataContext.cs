using Luftborn.Entities;
using Microsoft.EntityFrameworkCore;
namespace Luftborn.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
                builder.Entity<Product>()
            .HasOne(p => p.category)          
            .WithMany(c => c.Products)        
            .HasForeignKey(p => p.CategoryId) 
            .OnDelete(DeleteBehavior.Cascade);

    }




    public virtual DbSet<Product> Products { set; get; }
    public virtual DbSet<Category> Categories{ set; get; }




};

