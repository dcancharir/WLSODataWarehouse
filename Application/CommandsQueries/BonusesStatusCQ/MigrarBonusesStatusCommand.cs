using Application.CommandsQueries.BonusesCQ;
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

namespace Application.CommandsQueries.BonusesStatusCQ;
public class MigrarBonusesStatusCommand : IRequest<bool> {
    public class MigrarBonusesStatusCommandHandler : IRequestHandler<MigrarBonusesStatusCommand, bool> {
        private readonly IBonusesStatusRepository _bonusesStatusRepository;
        private readonly IDWBonusesStatusRepository _dwBonusesStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarBonusesStatusCommandHandler> _logger;

        public MigrarBonusesStatusCommandHandler(IBonusesStatusRepository bonusesStatusRepository, IDWBonusesStatusRepository dwBonusesStatusRepository, IMapper mapper, ILogger<MigrarBonusesStatusCommandHandler> logger) {
            _bonusesStatusRepository = bonusesStatusRepository;
            _dwBonusesStatusRepository = dwBonusesStatusRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool>Handle(MigrarBonusesStatusCommand request,CancellationToken cancellationToken) {
            var response = false;
            try {
                var registros = await _bonusesStatusRepository.GetAll();
                if(!registros.Any()) {
                    _logger.LogWarning($"MigrarBonusesStatusCommandHandler - No se encontraron registros a migrar");
                    return true;
                }
                var existentes = await _dwBonusesStatusRepository.GetListByFilter(x=>registros.Select(y=>y.Status).Contains(x.Status));

                var listaStatusId = existentes.Select(x => x.Status);

                var registrosMapear = registros.Where(x => !listaStatusId.Contains(x.Status));

                var registrosMapeados = _mapper.Map<List<DWBonusesStatus>>(registrosMapear);

                if(registrosMapear.Any()) {
                    await _dwBonusesStatusRepository.BulkInsert(registrosMapeados);
                    await _dwBonusesStatusRepository.BulkSaveChanges();
                }
                response = true;

            } catch(Exception) {
            
            }
            return response;

        }
    }
}
