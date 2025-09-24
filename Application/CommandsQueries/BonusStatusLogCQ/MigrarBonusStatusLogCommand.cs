using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.BonusStatusLogCQ;
public class MigrarBonusStatusLogCommand :IRequest<bool>{
    public class MigrarBonusStatusLogCommandHandler : IRequestHandler<MigrarBonusStatusLogCommand, bool> {
        private readonly IDWBonusStatusLogRepository _dwBonusStatusLogRepository;
        private readonly IBonusStatusLogRepository _bonusStatusLogRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarBonusStatusLogCommand> _logger;
        public MigrarBonusStatusLogCommandHandler(IDWBonusStatusLogRepository dwBonusStatusLogRepository, IBonusStatusLogRepository bonusStatusLogRepository, IMapper mapper, ILogger<MigrarBonusStatusLogCommand> logger) {
            _dwBonusStatusLogRepository = dwBonusStatusLogRepository;
            _bonusStatusLogRepository = bonusStatusLogRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool>Handle(MigrarBonusStatusLogCommand request,CancellationToken cancellationToken) {
            bool response = false;
            try {
                var remoto = await _bonusStatusLogRepository.GetAll();
                var registros = _mapper.Map<List<DWBonusStatusLog>>(remoto);

                var ids = registros.Select(x => x.LogId);
                var registrosExistentes = await _dwBonusStatusLogRepository.GetAll();

                var idsExistentes = registrosExistentes.Where(x=>ids.Contains(x.LogId)).Select(x=>x.LogId).ToList();
                if(idsExistentes.Any() ) {
                    registros.RemoveAll(x => idsExistentes.Contains(x.LogId));
                }

                if(registros.Count != 0) {
                    await _dwBonusStatusLogRepository.BulkInsert(registros);
                    await _dwBonusStatusLogRepository.BulkSaveChanges();
                }
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarBonusStatusLogCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
