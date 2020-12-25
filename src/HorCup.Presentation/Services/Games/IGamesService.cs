using System;
using System.Threading.Tasks;

namespace HorCup.Presentation.Services.Games
{
	public interface IGamesService
	{
		Task<bool> IsTitleUniqueAsync(string title, Guid? id);
	}
}