using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.RealGameEventCQ;
public class MigrarRealGameEventCommand3 : IRequest<bool> {
    public class MigrarRealGameEventCommandHandler : IRequestHandler<MigrarRealGameEventCommand3, bool> {
        private readonly IRealGameEventRepository _realGameEventRepository;
        private readonly IDWRealGameEventRepository _dwRealGameEventRepository;
        private readonly ILogger<MigrarRealGameEventCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        int LimitePorPaginacion;
        public MigrarRealGameEventCommandHandler(IRealGameEventRepository realGameEventRepository, IDWRealGameEventRepository dwRealGameEventRepository, ILogger<MigrarRealGameEventCommandHandler> logger, IMapper mapper, IConfiguration configuration) {
            _realGameEventRepository = realGameEventRepository;
            _dwRealGameEventRepository = dwRealGameEventRepository;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            LimitePorPaginacion = Convert.ToInt32(_configuration.GetSection("Variables")["LimitePorPaginacion"]);
        }
        public async Task<bool> Handle(MigrarRealGameEventCommand3 request, CancellationToken ct) {
            bool response = false;
            var batchSize = LimitePorPaginacion;
            ulong currentId = 0;
            try {
                var lastRecord = await _dwRealGameEventRepository.GetLastRecord();
                if(lastRecord != null) {
                    currentId = lastRecord.EventId;
                }
                var hoy = DateTime.Today;
                int iteration = 0;
                while(true) {
                    var batch = (await _realGameEventRepository.GetPaginatedByIdCursor(currentId, batchSize)).ToList();
                    if(!batch.Any()) break;

                    bool hayRegistrosDeHoy = batch.Any(x => x.InsDatetime.HasValue && x.InsDatetime.Value.Date >= hoy);
                    var paraInsertar = batch.Where(x => x.InsDatetime.HasValue && x.InsDatetime.Value.Date < hoy).ToList();

                    if(paraInsertar.Any()) {
                        var mapped = _mapper.Map<List<DWRealGameEvent>>(paraInsertar);
                        var eventsId = mapped.Select(x => x.EventId);
                        var exists = await _dwRealGameEventRepository.GetListByFilter(x => eventsId.Contains(x.EventId));

                        if(exists.Any()) {
                            var idsExists = exists.Select(x => x.EventId).ToList();
                            mapped.RemoveAll(x => idsExists.Contains(x.EventId));
                        }
                        if(mapped.Any()) {
                            await _dwRealGameEventRepository.BulkInsert(mapped);
                            await _dwRealGameEventRepository.BulkSaveChanges();
                        }
                    }

                    currentId = batch.Max(x => x.EventId);
                    _logger.LogInformation($"Limite paginacion : {batchSize} - Nro. Iteracion : {iteration} - LastId procesado : {currentId}");
                    iteration++;

                    if(hayRegistrosDeHoy) break;
                }
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarRealGameEventCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
