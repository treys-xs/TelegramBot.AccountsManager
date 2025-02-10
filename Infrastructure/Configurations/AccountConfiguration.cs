using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");
        
        builder.HasKey(account => account.Id)
            .HasName("account_id");

        builder.Property(account => account.Login)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("login");
        
        builder.Property(account => account.Password)
            .HasMaxLength(100)
            .IsRequired()
            .HasColumnName("password");
        
        builder.Property(account => account.Description)
            .HasMaxLength(500)
            .HasColumnName("description");

        builder.Property(account => account.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.HasOne(account => account.Service)
            .WithMany(service => service.Accounts);
    }
}