using System;
using System.Linq;
using HorCup.Infrastructure.ViewModels;
using HorCup.Plays.Context;
using HorCup.Plays.Models;
using MongoDB.Driver;
using RandomNameGeneratorLibrary;

namespace HorCup.Plays;

internal class DbInitializer
{
	public static void Initialize(IPlaysContext context)
	{
		if (!context.Plays.Find(_ => true).Any())
		{
			var personGenerator = new PersonNameGenerator();

			var placeGenerator = new PlaceNameGenerator();

			var random = new Random(250);

			var plays = Enumerable.Range(1, 250)
				.Select(i => new Play
				{
					Game = new Game
					{
						Id = Guid.NewGuid(),
						Title = placeGenerator.GenerateRandomPlaceName()
					},
					Id = Guid.NewGuid(),
					Notes = placeGenerator.GenerateRandomPlaceName(),
					PlayedDate = DateTime.Now.AddDays(-i),
					PlayerScores = new PlayScore[]
					{
						new()
						{
							Player = new IdName(Guid.NewGuid(), personGenerator.GenerateRandomFirstAndLastName()),
							Score = random.Next()
						},

						new()
						{
							Player = new IdName(Guid.NewGuid(), personGenerator.GenerateRandomFirstAndLastName()),
							Score = random.Next()
						},

						new()
						{
							Player = new IdName(Guid.NewGuid(),
								personGenerator
									.GenerateRandomFirstAndLastName()),
							Score = random.Next()
						},
					}
				});

			context.Plays.InsertMany(plays);
		}
	}
}