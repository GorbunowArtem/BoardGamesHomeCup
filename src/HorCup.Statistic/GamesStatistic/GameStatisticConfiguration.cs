using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorCup.Statistic.GamesStatistic
{
	public class GameStatisticConfiguration : IEntityTypeConfiguration<GameStatistic>
	{
		public void Configure(EntityTypeBuilder<GameStatistic> builder)
		{
			builder.HasKey(gs => gs.Id);

			builder.Property(p => p.Id).ValueGeneratedOnAdd();
			
			builder.Property(gs => gs.LastPlayedDate)
				.HasColumnType("datetime");
		}
	}
}