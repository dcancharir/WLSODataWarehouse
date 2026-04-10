using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.RealGameEventCQ;
public class MigrarRealGameEventCommand2 : IRequest<bool> {
    public DateTime fechaOperacion {  get; set; }
    public class MigrarRealGameEventCommand2Handler : IRequestHandler<MigrarRealGameEventCommand2, bool> {
        private readonly IDWRealGameEventRepository _dwRealGameEventRepository;
        private readonly IRealGameEventRepository _realGameEventRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarRealGameEventCommand2> _logger;
        private readonly IDWHistorialMigracionWSLORepository _dwHistorialMigracionWLSORepository;
        public MigrarRealGameEventCommand2Handler(IDWRealGameEventRepository dwRealGameEventRepository, IRealGameEventRepository realGameEventRepository, IMapper mapper, ILogger<MigrarRealGameEventCommand2> logger, IDWHistorialMigracionWSLORepository dwHistorialMigracionWLSORepository) {
            _dwRealGameEventRepository = dwRealGameEventRepository;
            _realGameEventRepository = realGameEventRepository;
            _mapper = mapper;
            _logger = logger;
            _dwHistorialMigracionWLSORepository = dwHistorialMigracionWLSORepository;
        }
        public async Task<bool> Handle(MigrarRealGameEventCommand2 request, CancellationToken cancellationToken) {
            bool response = false;
        
            try {
                var fechaOperacion = request.fechaOperacion;
                var fechaHistorialEditar = await _dwHistorialMigracionWLSORepository.FirstOrDefault(x => x.fechaoperacion == fechaOperacion.Date);
                if(fechaHistorialEditar == null) {
                    return false;
                }
                if(fechaHistorialEditar.realgameevents == 1) {
                    return true;
                }
                var fechaOperacionDate = DateOnly.FromDateTime(fechaOperacion);

                var itemsEliminar = await _dwRealGameEventRepository.GetQuery(null, x => x.InsDatetime.Value.Date == fechaOperacion.Date);

                if(itemsEliminar != null) {
                    await _dwRealGameEventRepository.RemoveRange(itemsEliminar.ToList());
                }

                var itemsMysql = await _realGameEventRepository.GetQuery(null, x => x.InsDatetime.Value.Date == fechaOperacion.Date);
                if(itemsMysql != null) {
                    var registrosMapeados = _mapper.Map<List<DWRealGameEvent>>(itemsMysql);
                    await _dwRealGameEventRepository.BulkInsert(registrosMapeados);
                    await _dwRealGameEventRepository.BulkSaveChanges();
                }
                //cerrar dia para esa tabla

                fechaHistorialEditar.realgameevents = 1;
                await _dwHistorialMigracionWLSORepository.UpdateAndSave(fechaHistorialEditar);

                _logger.LogInformation($"MigrarRealGameEventCommand2Handler - Dia cerrado para :  {fechaOperacion}");
                response = true;

            } catch(Exception ex) {
                _logger.LogError($"MigrarRealGameEventCommand2Handler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}

