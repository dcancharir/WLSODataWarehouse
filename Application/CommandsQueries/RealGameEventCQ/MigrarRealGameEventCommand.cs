using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Linq.Expressions;

namespace Application.CommandsQueries.RealGameEventCQ;
public class MigrarRealGameEventCommand : IRequest<bool>{
    public class MigrarRealGameEventCommandHandler : IRequestHandler<MigrarRealGameEventCommand, bool> {
        private readonly IDWRealGameEventRepository _dwRealGameEventRepository;
        private readonly IRealGameEventRepository _realGameEventRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarRealGameEventCommand> _logger;
        private readonly IConfiguration _configuration;
        private int LimitePorPaginacion;
        public MigrarRealGameEventCommandHandler(IDWRealGameEventRepository dwRealGameEventRepository, IRealGameEventRepository realGameEventRepository, IMapper mapper, ILogger<MigrarRealGameEventCommand> logger, IConfiguration configuration) {
            _dwRealGameEventRepository = dwRealGameEventRepository;
            _realGameEventRepository = realGameEventRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            LimitePorPaginacion = Convert.ToInt32(_configuration.GetSection("Variables")["LimitePorPaginacion"]);
        }
        public async Task<bool> Handle(MigrarRealGameEventCommand request, CancellationToken cancellationToken) {
            bool response = false;
            var batchSize = LimitePorPaginacion;
            DateTime lastDate = new DateTime(year: 1753, month: 1, day: 1);//fecha minima en sql server, mysql acepta desde el 1000-01-01
            try {
                var lastRecord = await _dwRealGameEventRepository.GetLastRecordByDate();
                if(lastRecord != null) {
                    lastDate = Convert.ToDateTime(lastRecord.InsDatetime);
                }
                var totalRecords = await _realGameEventRepository.GetTotalRecordsByDate(lastDate);
                var batchCount = (totalRecords +  batchSize - 1)/batchSize;
                for(int i = 0; i<= batchCount; i++) {
                    var startIndex = i * batchSize;
                    var batch = await _realGameEventRepository.GetPaginatedByDates(startIndex, batchSize, lastDate);
                    var mapped = _mapper.Map<List<DWRealGameEvent>>(batch);

                    var eventsIds = mapped.Select(x => x.EventId);
                    var registrosExistentes = await _dwRealGameEventRepository.GetListByFilter(x => eventsIds.Contains(x.EventId));

                    if(registrosExistentes.Any()) {
                        var idsExistentes = registrosExistentes.Select(x => x.EventId);
                        mapped.RemoveAll(x => idsExistentes.Contains(x.EventId));
                    }
                    if(mapped.Count != 0) {
                        await _dwRealGameEventRepository.BulkInsert(mapped);
                        await _dwRealGameEventRepository.SaveChanges();
                    }
                    //foreach (var item in mapped) {
                    //    Expression<Func<DWRealGameEvent, bool>> predicate = c => item.EventId == c.EventId;
                    //    await _dwRealGameEventRepository.AddIfNotExist(item,predicate);
                    //    await _dwRealGameEventRepository.SaveChanges();
                    //}
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
