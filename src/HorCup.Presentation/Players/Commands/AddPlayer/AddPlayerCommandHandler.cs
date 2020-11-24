using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HorCup.Presentation.Context;
using HorCup.Presentation.Services.IdGenerator;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.Players.Commands.AddPlayer
{
	public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, PlayerViewModel>
	{
		private readonly HorCupContext _context;
		private readonly IIdGenerator _idGenerator;
		private readonly IMapper _mapper;
		private readonly ILogger<AddPlayerCommandHandler> _logger;

		public AddPlayerCommandHandler(
			HorCupContext context,
			IIdGenerator idGenerator,
			IMapper mapper,
			ILogger<AddPlayerCommandHandler> logger)
		{
			_context = context;
			_idGenerator = idGenerator;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<PlayerViewModel> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
		{
			var id = _idGenerator.NewGuid();

			var player = new Player
			{
				Id = id,
				FirstName = request.FirstName,
				LastName = request.LastName,
				BirthDate = request.BirthDate
			};

			await _context.AddAsync(player, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<PlayerViewModel>(player);
		}
	}
}