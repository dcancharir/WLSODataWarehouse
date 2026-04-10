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
public class MigrarBonusStatusLogCommand2 : IRequest<bool> {
    public DateTime fechaOperacion { get; set; }
    public class MigrarBonusStatusLogCommand2Handler : IRequestHandler<MigrarBonusStatusLogCommand2, bool> {
        private readonly IDWBonusStatusLogRepository _dwBonusStatusLogRepository;
        private readonly IBonusStatusLogRepository _bonusStatusLogRepository;
        private readonly IDWHistorialMigracionWSLORepository _dwHistorialMigracionWLSORepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarBonusStatusLogCommand2> _logger;
        public MigrarBonusStatusLogCommand2Handler(IDWBonusStatusLogRepository dwBonusStatusLogRepository, IBonusStatusLogRepository bonusStatusLogRepository, IMapper mapper, ILogger<MigrarBonusStatusLogCommand2> logger, IDWHistorialMigracionWSLORepository dwHistorialMigracionWLSORepository) {
            _dwBonusStatusLogRepository = dwBonusStatusLogRepository;
            _bonusStatusLogRepository = bonusStatusLogRepository;
            _mapper = mapper;
            _logger = logger;
            _dwHistorialMigracionWLSORepository = dwHistorialMigracionWLSORepository;
        }
        public async Task<bool> Handle(MigrarBonusStatusLogCommand2 request, CancellationToken cancellationToken) {
            bool response = false;
            try {
                var fechaOperacion = request.fechaOperacion;
                var fechaHistorialEditar = await _dwHistorialMigracionWLSORepository.FirstOrDefault(x=>x.fechaoperacion == fechaOperacion.Date);
                if(fechaHistorialEditar == null) {
                    return false;
                }
                if(fechaHistorialEditar.bonusstatuslog == 1) {
                    return true;
                }
                var fechaOperacionDate = DateOnly.FromDateTime(fechaOperacion);
                var itemsEliminar = await _dwBonusStatusLogRepository.GetQuery(null,x=>x.SetDate == fechaOperacionDate);
                if(itemsEliminar != null) {
                    await _dwBonusStatusLogRepository.RemoveRange(itemsEliminar.ToList());
                }
                var itemsMysql = await _bonusStatusLogRepository.GetQuery(null, x => x.SetDate == fechaOperacionDate);
                if(itemsMysql != null) {
                    var registrosMapeados = _mapper.Map<List<DWBonusStatusLog>>(itemsMysql);
                    await _dwBonusStatusLogRepository.BulkInsert(registrosMapeados);
                    await _dwBonusStatusLogRepository.BulkSaveChanges();
                }
                //cerrar dia para esa tabla
                fechaHistorialEditar.bonusstatuslog = 1;
                await _dwHistorialMigracionWLSORepository.UpdateAndSave(fechaHistorialEditar);
                _logger.LogInformation($"MigrarBonusStatusLogCommand2Handler - Dia cerrado para :  {fechaOperacion}");
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarBonusStatusLogCommand2Handler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
