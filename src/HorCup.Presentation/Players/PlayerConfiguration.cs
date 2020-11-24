using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorCup.Presentation.Players
{
	public class PlayerConfiguration: IEntityTypeConfiguration<Player>
	{
		public void Configure(EntityTypeBuilder<Player> builder)
		{
			var constraints = new PlayerConstraints();
			
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Id).ValueGeneratedOnAdd();

			builder.Property(p => p.FirstName)
				.IsRequired()
				.HasMaxLength(constraints.MaxNameLength);

			builder.Property(p => p.LastName)
				.IsRequired()
				.HasMaxLength(constraints.MaxNameLength);

			builder.Property(p => p.BirthDate)
				.IsRequired()
				.HasColumnType("datetime");
		}
	}
}