using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.CommandsQueries.BonusesCQ;
public class MigrarBonusesCommand : IRequest<bool>{
    public class MigrarBonusesCommandHandler : IRequestHandler<MigrarBonusesCommand, bool> {
        private readonly IDWBonusesRepository _dwBonusesRepository;
        private readonly IBonusesRepository _bonusesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarBonusesCommandHandler> _logger;
        private readonly IConfiguration _configuration;
        private int LimitePorPaginacion;
        public MigrarBonusesCommandHandler(IDWBonusesRepository dwBonusesRepository, IBonusesRepository bonusesRepository, IMapper mapper, ILogger<MigrarBonusesCommandHandler> logger, IConfiguration configuration) {
            _dwBonusesRepository = dwBonusesRepository;
            _bonusesRepository = bonusesRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            LimitePorPaginacion = Convert.ToInt32(_configuration.GetSection("Variables")["LimitePorPaginacion"]);
        }
        public async Task<bool> Handle(MigrarBonusesCommand request,CancellationToken cancellationToken) {
            bool response = false;
            var batchSize = LimitePorPaginacion;
            long lastTimestamp = 0;
            try {
                var lastRecord = await _dwBonusesRepository.GetLastRecordByInsTimestamp();
                if(lastRecord != null) {
                    lastTimestamp = lastRecord.InsTimestamp??0;
                }
                var totalRecords = await _bonusesRepository.GetTotalRecordsByTimestamp(lastTimestamp);
                var batchCount = (totalRecords + batchSize - 1)/batchSize;

                for(int i = 0; i<= batchCount; i++) {
                    var startIndex = i*batchSize;
                    var batch = await _bonusesRepository.GetPaginatedByTimestamp(startIndex, batchSize, lastTimestamp);

                    var mapped = _mapper.Map<List<DWBonuse>>(batch);

                    var bonuseIds = mapped.Select(x => x.BonusId);
                    var registrosExistentes = await _dwBonusesRepository.GetListByFilter(x => bonuseIds.Contains(x.BonusId));

                    if(registrosExistentes.Any()) {
                        var idsExistentes = registrosExistentes.Select(x => x.BonusId);
                        mapped.RemoveAll(x=>idsExistentes.Contains(x.BonusId));
                    }

                    if(mapped.Count != 0) {
                        await _dwBonusesRepository.BulkInsert(mapped);
                        await _dwBonusesRepository.SaveChanges();
                    }
                }
                response = true;
            } catch(Exception ex) {
                response = false;
                _logger.LogError($"MigrarBonusesCommandHandler - {ex.Message}");
            }
            return response;
        }
    }
}
