using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyMusicWebAPI.EFService;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasOne(c => c.CreatebyUser).WithMany()
            .HasForeignKey(c => c.CreatebyUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.UpdatebyUser).WithMany()
            .HasForeignKey(c => c.UpdatebyUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.CountryofbirthCountry).WithMany()
            .HasForeignKey(c => c.CountryofbirthCountryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.NationalityCountry).WithMany()
            .HasForeignKey(c => c.NationalityCountryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}