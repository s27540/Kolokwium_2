using WebApplication1.Models;

namespace WebApplication1.Context;

using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
public class SubscriptionContext : DbContext
{
    public SubscriptionContext(DbContextOptions<SubscriptionContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Discount> Discounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
        });


        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TotalPaidAmount).IsRequired().HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RenewalPeriod).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();

            entity.HasMany(e => e.Payments)
                  .WithOne(p => p.Subscription)
                  .HasForeignKey(p => p.SubscriptionId);
        });


        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentDate).IsRequired();

            entity.HasOne(p => p.Client)
                  .WithMany(c => c.Subscriptions)
                  .HasForeignKey(p => p.ClientId);

            entity.HasOne(p => p.Subscription)
                  .WithMany(s => s.Payments)
                  .HasForeignKey(p => p.SubscriptionId);
        });


        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).IsRequired().HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();

           
            entity.HasOne(d => d.Subscription)
                .WithMany() 
                .HasForeignKey(d => d.SubscriptionId);
        });
    }
}
