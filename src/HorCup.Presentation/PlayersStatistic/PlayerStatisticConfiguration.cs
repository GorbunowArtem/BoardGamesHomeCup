using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorCup.Presentation.PlayersStatistic
{
	public class PlayerStatisticConfiguration: IEntityTypeConfiguration<PlayerStatistic>
	{
		public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
		{
			builder.HasKey(k => new
			{
				k.GameId,
				k.PlayerId
			});
		}
	}
}