using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyMusicWebAPI.EFService;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(c => c.CreatebyUser).WithMany()
            .HasForeignKey(c => c.CreatebyUserId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.UpdatebyUser).WithMany()
            .HasForeignKey(c => c.UpdatebyUserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}