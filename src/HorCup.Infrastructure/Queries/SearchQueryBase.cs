namespace HorCup.Infrastructure.Queries;

public record SearchQueryBase
{
	public int Take { get; set; } = 10;

	public int Skip { get; set; }
}