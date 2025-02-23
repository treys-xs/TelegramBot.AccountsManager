using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Infrastructure.Configurations;

public class UserStateConfiguration : IEntityTypeConfiguration<UserState>
{
    public void Configure(EntityTypeBuilder<UserState> builder)
    {
        builder.ToTable("user_states");

        builder.HasKey(state => state.Id);

        builder
            .Property(state => state.Id)
            .HasColumnName("user_state_id");

        builder
            .Property(state => state.Name)
            .HasColumnName("name");
        
        builder
            .Property(state => state.Step)
            .HasColumnName("step");

        builder
            .HasOne(state => state.User)
            .WithOne(user => user.State)
            .HasForeignKey<UserState>(state => state.UserId)
            .IsRequired();
    }
}