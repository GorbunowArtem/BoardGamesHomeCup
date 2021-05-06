using HorCup.Plays.Models;
using MongoDB.Driver;

namespace HorCup.Plays.Context
{
	public interface IPlaysContext
	{
		IMongoCollection<Play> Plays { get; }
	}
}