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
            ulong lastId = 0;
            try {
                var fechaActual = DateTime.Now;
                var lastRecord = await _dwRealGameEventRepository.GetLastRecord();
                if(lastRecord != null) {
                    lastId = lastRecord.EventId;
                }
                var totalRecords = await _realGameEventRepository.GetTotalRecordsById(lastId);
                var batchCount = (totalRecords + batchSize - 1) / batchSize;
                for(int i = 0; i < batchCount; i++) {
                    var startIndex = i * batchSize;
                    var batch = await _realGameEventRepository.GetPaginatedById(startIndex,batchSize,lastId);

                    ////No puede insertarse registros de hoy, si estamos 08/mayo/2026, solo pueden insertarse registros de hasta el 07 mayo
                    //if(batch.Any(x => x.InsDatetime.Value.Date == fechaActual.Date)) {
                    //    _logger.LogError($"Fecha Actual : {fechaActual.Date}; existe un registro con esta fecha");
                    //    break;
                    //}

                    var mapped = _mapper.Map<List<DWRealGameEvent>>(batch);
                    var eventsId = mapped.Select(x => x.EventId);
                    var exists = await _dwRealGameEventRepository.GetListByFilter(x=>eventsId.Contains(x.EventId));

                    if(exists.Any()) {
                        var idsExists = exists.Select(x => x.EventId).ToList();
                        mapped.RemoveAll(x=>idsExists.Contains(x.EventId));
                    }
                    if(mapped.Any()) { 
                        await _dwRealGameEventRepository.BulkInsert(mapped);
                        await _dwRealGameEventRepository.BulkSaveChanges();
                    }
                    _logger.LogInformation($"Limite paginacion : {batchSize} - Nro. Iteracion : {i} - StartIndex : {startIndex} - LastId : {lastId}");
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
