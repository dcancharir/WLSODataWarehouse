using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Application.CommandsQueries.RealGameEventCQ;
public class MigrarRealGameEventCommand : IRequest<bool>{
    public class MigrarRealGameEventCommandHandler : IRequestHandler<MigrarRealGameEventCommand, bool> {
        private readonly IDWRealGameEventRepository _dwRealGameEventRepository;
        private readonly IRealGameEventRepository _realGameEventRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarRealGameEventCommand> _logger;
        public MigrarRealGameEventCommandHandler(IDWRealGameEventRepository dwRealGameEventRepository, IRealGameEventRepository realGameEventRepository, IMapper mapper, ILogger<MigrarRealGameEventCommand> logger) {
            _dwRealGameEventRepository = dwRealGameEventRepository;
            _realGameEventRepository = realGameEventRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarRealGameEventCommand request, CancellationToken cancellationToken) {
            bool response = false;
            var batchSize = 1000;
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
                    foreach (var item in mapped) {
                        Expression<Func<DWRealGameEvent, bool>> predicate = c => item.EventId == c.EventId;
                        await _dwRealGameEventRepository.AddIfNotExist(item,predicate);
                        await _dwRealGameEventRepository.SaveChanges();
                    }
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
