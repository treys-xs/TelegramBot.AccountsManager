using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("services");
        
        builder
            .HasKey(service => service.Id)
            .HasName("service_id");
        
        builder
            .Property(service => service.Name)
            .HasMaxLength(100)
            .HasColumnName("name")
            .IsRequired();
        
        builder
            .Property(service => service.Description)
            .HasMaxLength(500)
            .HasColumnName("description");
        
        builder
            .Property(service => service.Url)
            .HasMaxLength(100)
            .HasColumnName("url");

        builder
            .HasOne(service => service.User)
            .WithMany(user => user.Services);
        
        builder
            .HasMany(service => service.Accounts)
            .WithOne(user => user.Service);
    }
}