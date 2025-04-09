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

namespace Application.CommandsQueries.PlayerCQ;
public class MigrarPlayersCommand : IRequest<bool>{
    public class MigrarPlayersCommandHandler : IRequestHandler<MigrarPlayersCommand, bool> {
        private readonly IPlayerRepository _playerRepository;
        private readonly IDWPlayerRepository _dwPlayerRepository;
        private readonly ILogger<MigrarPlayersCommandHandler> _logger;
        private readonly IMapper _mapper;
        public MigrarPlayersCommandHandler(IPlayerRepository playerRepository, IDWPlayerRepository dwPlayerRepository, ILogger<MigrarPlayersCommandHandler> logger, IMapper mapper) {
            _playerRepository = playerRepository;
            _dwPlayerRepository = dwPlayerRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(MigrarPlayersCommand request, CancellationToken cancellationToken) {
            bool response = false;
            var batchSize = 1000;
            uint lastId = 0;
            try {
                var lastRecord = await _dwPlayerRepository.GetLastRecord();
                if(lastRecord != null) {
                    lastId = lastRecord.PlayerId;
                }
                var totalRecords = await _playerRepository.GetTotalRecordsById(lastId);
                var batchCount = (totalRecords + batchSize - 1)/batchSize;
                for(int i = 0; i <= batchCount; i++) {
                    var startIndex = i* batchSize;
                    var batch = await _playerRepository.GetPaginatedById(startIndex, batchSize, lastId);
                    var mapped = _mapper.Map<List<DWPlayer>>(batch);
                    foreach(var item in mapped) {
                        Expression<Func<DWPlayer,bool>> predicate = c => c.PlayerId == item.PlayerId;
                        await _dwPlayerRepository.AddIfNotExist(item,predicate);
                        await _dwPlayerRepository.SaveChanges();
                    }
                }
                response = true;

            } catch(Exception ex) {
                _logger.LogError($"MigrarPlayersCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
