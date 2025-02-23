using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);
        
        builder
            .Property(user => user.Id)
            .HasColumnName("user_id");
        
        builder
            .Property(user => user.TelegramId)
            .HasColumnName("telegram_id")
            .IsRequired();
        
        builder
            .Property(user => user.Nickname)
            .HasColumnName("nickname")
            .IsRequired();
        
        builder
            .Property(user => user.Password)
            .HasColumnName("password");
        
        builder
            .Property(user => user.IsSubscription)
            .HasColumnName("is_subscription")
            .IsRequired()
            .HasDefaultValue(true);
        
        builder
            .HasMany(user => user.Services)
            .WithOne(service => service.User);
    }
}