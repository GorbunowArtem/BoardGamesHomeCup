using HorCup.Players.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorCup.Players.Players
{
	public class PlayerConfiguration: IEntityTypeConfiguration<Player>
	{
		public void Configure(EntityTypeBuilder<Player> builder)
		{
			var constraints = new PlayerConstraints();
			
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Id).ValueGeneratedOnAdd();

			builder.Property(p => p.Nickname)
				.IsRequired()
				.HasMaxLength(constraints.MaxNameLength);

			builder.Property(p => p.Added);
		}
	}
}