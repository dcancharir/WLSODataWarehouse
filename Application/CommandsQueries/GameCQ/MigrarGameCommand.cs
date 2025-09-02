using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.GameCQ;
public class MigrarGameCommand : IRequest<bool>{
    public class MigrarGameCommandHandler : IRequestHandler<MigrarGameCommand, bool> {
        private readonly IDWGameRepository _dwGameRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<MigrarGameCommandHandler> _logger;
        private readonly IMapper _mapper;
        public MigrarGameCommandHandler(IDWGameRepository dwGameRepository, IGameRepository gameRepository, ILogger<MigrarGameCommandHandler> logger, IMapper mapper) {
            _dwGameRepository = dwGameRepository;
            _gameRepository = gameRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(MigrarGameCommand request, CancellationToken cancellationToken) {
            bool response = false;
            try {
                var registros = await _gameRepository.GetAll();
                if (!registros.Any()) {
                    _logger.LogWarning($"MigrarGameCommandHandler - No se encontraron registros a migrar");
                    return true;
                }
                var existentes = await _dwGameRepository
                    .GetListByFilter(x => 
                        registros.Select(y => y.GameId).Contains(x.GameId) && 
                        registros.Select(y => y.ProviderId).Contains(x.ProviderId));

                var listaGamesId = existentes.Select(x => x.GameId);
                var listaProviderIds = existentes.Select(x => x.ProviderId);

                var registrosMapear = registros.Where(x => !listaGamesId.Contains(x.GameId) && !listaProviderIds.Contains(x.ProviderId));

                var registrosMapeados = _mapper.Map<List<DWGame>>(registrosMapear);

                if(registrosMapeados.Any()) { 
                    await _dwGameRepository.BulkInsert(registrosMapeados);
                    await _dwGameRepository.BulkSaveChanges();
                }
                //var remoto = await _gameRepository.GetAll();
                //var registros = _mapper.Map<List<DWGame>>(remoto);
                //foreach(var item in registros) {
                //    Expression<Func<DWGame, bool>> predicate = c => c.GameId == item.GameId && c.ProviderId == item.ProviderId;
                //    await _dwGameRepository.AddIfNotExist(item, predicate);
                //    await _dwGameRepository.SaveChanges();
                //}
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarGameCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
