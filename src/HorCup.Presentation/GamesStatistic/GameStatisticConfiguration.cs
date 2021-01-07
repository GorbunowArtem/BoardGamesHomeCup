using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorCup.Presentation.GamesStatistic
{
	public class GameStatisticConfiguration : IEntityTypeConfiguration<GameStatistic>
	{
		public void Configure(EntityTypeBuilder<GameStatistic> builder)
		{
			builder.HasKey(gs => gs.GameId);

			builder.Property(gs => gs.LastPlayedDate)
				.HasColumnType("datetime");
		}
	}
}