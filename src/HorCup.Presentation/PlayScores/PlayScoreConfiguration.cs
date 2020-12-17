using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorCup.Presentation.PlayScores
{
	public class PlayScoreConfiguration : IEntityTypeConfiguration<PlayScore>
	{
		public void Configure(EntityTypeBuilder<PlayScore> builder)
		{
			builder.HasKey(ps => ps.Id);

			builder.Property(ps => ps.Id).ValueGeneratedOnAdd();

			builder.HasOne(ps => ps.Player)
				.WithMany(ps => ps.PlayScores)
				.HasForeignKey(ps => ps.PlayerId);
		}
	}
}