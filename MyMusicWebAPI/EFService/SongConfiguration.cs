using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyMusicWebAPI.EFService;

internal class SongConfiguration : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.HasOne(c => c.CreatebyUser)
            .WithMany()
            .HasForeignKey(c => c.CreatebyUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.UpdatebyUser)
           .WithMany()
           .HasForeignKey(c => c.UpdatebyUserId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.Artist)
           .WithMany()
           .HasForeignKey(c => c.ArtistId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.ComposerArtist)
           .WithMany()
           .HasForeignKey(c => c.ComposerArtistId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.LyricistArtist)
           .WithMany()
           .HasForeignKey(c => c.LyricistArtistId)
           .OnDelete(DeleteBehavior.NoAction);
    }
}