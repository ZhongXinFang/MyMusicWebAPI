using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyMusicWebAPI.EFService;

public class LyricConfiguration : IEntityTypeConfiguration<Lyric>
{
    public void Configure(EntityTypeBuilder<Lyric> builder)
    {
        builder.HasOne(c => c.CreatebyUser)
            .WithMany()
            .HasForeignKey(c => c.CreatebyUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(c => c.UpdatebyUser)
            .WithMany()
            .HasForeignKey(c => c.UpdatebyUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(c => c.LyricistUser)
            .WithMany()
            .HasForeignKey(c => c.LyricistUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(c => c.Language)
            .WithMany()
            .HasForeignKey(c => c.LanguageId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(c => c.Song)
            .WithMany(s => s.Lyrics)
            .HasForeignKey(c => c.SongId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}