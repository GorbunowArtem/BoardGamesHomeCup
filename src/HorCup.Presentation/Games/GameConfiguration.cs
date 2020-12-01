using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorCup.Presentation.Games
{
	public class GameConfiguration : IEntityTypeConfiguration<Game>
	{
		public void Configure(EntityTypeBuilder<Game> builder)
		{
			var constraints = new GamesConstraints();

			builder.HasKey(g => g.Id);

			builder.Property(g => g.Id).ValueGeneratedOnAdd();

			builder.Property(g => g.Title)
				.IsRequired()
				.HasMaxLength(constraints.TitleMaxLength);

			builder.Property(g => g.MaxPlayers)
				.IsRequired();
		}
	}
}