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

namespace Application.CommandsQueries.CustomerCQ;
public class MigrarCustomerCommand2 : IRequest<bool> {
    public DateTime fechaOperacion { get; set; }
    public class MigrarCustomerCommand2Handler : IRequestHandler<MigrarCustomerCommand2, bool> {
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarCustomerCommand2Handler> _logger;
        private readonly IDWCustomerRepository _dwCustomerRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDWHistorialMigracionWSLORepository _dwHistorialMigracionWLSORepository;
        public MigrarCustomerCommand2Handler(IMapper mapper, ILogger<MigrarCustomerCommand2Handler> logger, IDWCustomerRepository dwCustomerRepository, ICustomerRepository customerRepository, IConfiguration configuration, IDWHistorialMigracionWSLORepository dwHistorialMigracionWLSORepository) {
            _mapper = mapper;
            _logger = logger;
            _dwCustomerRepository = dwCustomerRepository;
            _customerRepository = customerRepository;
            _dwHistorialMigracionWLSORepository = dwHistorialMigracionWLSORepository;
        }
        public async Task<bool> Handle(MigrarCustomerCommand2 request, CancellationToken cancellationToken) {
            bool response = false;
            try {
                var fechaOperacion = request.fechaOperacion;
                var fechaHistorialEditar = await _dwHistorialMigracionWLSORepository.FirstOrDefault(x => x.fechaoperacion == fechaOperacion.Date);
                if(fechaHistorialEditar == null) {
                    return false;
                }
                if(fechaHistorialEditar.customers == 1) {
                    return true;
                }
                var fechaOperacionDate = DateOnly.FromDateTime(fechaOperacion);

                var itemsEliminar = await _dwCustomerRepository.GetQuery(null, x => x.RegDate.Value.Date == fechaOperacion.Date);
                if (itemsEliminar != null)
                {
                    await _dwCustomerRepository.RemoveRange(itemsEliminar.ToList());
                }

                var itemsMysql = await _customerRepository.GetQuery(null, x => x.RegDate.Value.Date == fechaOperacion.Date);
                if (itemsMysql !=null)
                {
                    var registrosMapeados = _mapper.Map<List<DWCustomer>>(itemsMysql);
                    await _dwCustomerRepository.BulkInsert(registrosMapeados);
                    await _dwCustomerRepository.BulkSaveChanges();
                }

                //cerra dia para esa tabla

                fechaHistorialEditar.customers = 1;
                await _dwHistorialMigracionWLSORepository.UpdateAndSave(fechaHistorialEditar);

                _logger.LogInformation($"MigrarCustomerCommand2Handler - Dia cerrado para :  {fechaOperacion}");
                
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarCustomerCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
