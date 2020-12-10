using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorCup.Presentation.Plays
{
	public class PlayConfiguration: IEntityTypeConfiguration<Play>
	{
		public void Configure(EntityTypeBuilder<Play> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Id).ValueGeneratedOnAdd();

			builder.HasOne(p => p.Game)
				.WithOne(p => p.Play);
			
			builder.Property(p => p.PlayedDate)
				.HasColumnType("datetime");
		}
	}
}