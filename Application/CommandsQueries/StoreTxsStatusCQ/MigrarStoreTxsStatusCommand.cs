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

namespace Application.CommandsQueries.StoreTxsStatusCQ;
public class MigrarStoreTxsStatusCommand : IRequest<bool>{
    public class MigrarStoreTxsStatusCommandHandler : IRequestHandler<MigrarStoreTxsStatusCommand,bool> {
        private readonly IDWStoreTxsStatusRepository _dwStoreTxsStatusRepository;
        private readonly IStoreTxsStatusRepository _storeTxsStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarStoreTxsStatusCommandHandler> _logger;
        public MigrarStoreTxsStatusCommandHandler(IDWStoreTxsStatusRepository dwStoreTxsStatusRepository, IStoreTxsStatusRepository storeTxsStatusRepository, IMapper mapper, ILogger<MigrarStoreTxsStatusCommandHandler> logger) {
            _dwStoreTxsStatusRepository = dwStoreTxsStatusRepository;
            _storeTxsStatusRepository = storeTxsStatusRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool>Handle(MigrarStoreTxsStatusCommand request,CancellationToken cancellationToken) {
            bool response = false;
            try {
                var totalMysql = await _storeTxsStatusRepository.GetCountAll();
                var totalSql = await _dwStoreTxsStatusRepository.GetCountAll();
                if(totalMysql != totalSql) {
                    _logger.LogWarning($"MigrarStoreTxsStatusCommandHandler - No se encontraton cambios en tablas");
                    return true;
                }

                var registros = await _storeTxsStatusRepository.GetAll();
                if(!registros.Any()) {
                    _logger.LogWarning($"MigrarStoreTxsStatusCommandHandler - No se encontraron registros a migrar");
                    return true;
                }

                var existentes = await _dwStoreTxsStatusRepository.GetListByFilter(x => registros.Select(y => y.Status).Contains(x.Status));

                var listaRucs = existentes.Select(x => x.Status);

                var registrosMapear = registros.Where(x => !listaRucs.Contains(x.Status));
                var registrosMapeados = _mapper.Map<List<DWStoreTxsStatus>>(registrosMapear);

                if(registrosMapeados.Any()) {
                    await _dwStoreTxsStatusRepository.BulkInsert(registrosMapeados);
                    await _dwStoreTxsStatusRepository.BulkSaveChanges();
                }
                response = true;

            } catch(Exception ex) {
                response = false;
                _logger.LogError($"MigrarStoreTxsStatusCommandHandler - {ex.Message}");
            }
            return response;
        }
    }
}
