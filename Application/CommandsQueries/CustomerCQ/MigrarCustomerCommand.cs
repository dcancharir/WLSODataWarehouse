using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Application.CommandsQueries.CustomerCQ;
public class MigrarCustomerCommand :IRequest<bool>{
    public class MigrarCustomerCommandHandler : IRequestHandler<MigrarCustomerCommand, bool> {
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarCustomerCommandHandler> _logger;
        private readonly IDWCustomerRepository _dwCustomerRepository;
        private readonly ICustomerRepository _customerRepository;
        public MigrarCustomerCommandHandler(IMapper mapper, ILogger<MigrarCustomerCommandHandler> logger, IDWCustomerRepository dwCustomerRepository, ICustomerRepository customerRepository) {
            _mapper = mapper;
            _logger = logger;
            _dwCustomerRepository = dwCustomerRepository;
            _customerRepository = customerRepository;
        }
        public async Task<bool> Handle(MigrarCustomerCommand request,CancellationToken cancellationToken) {
            bool response = false;
            var delay = 1000;
            bool continuarTarea = true;
            try {
                while(continuarTarea) {
                    continuarTarea = false;
                    await Task.Delay(delay);
                    const int totalItems = 1000;
                    DateTime lastDate = new DateTime(year: 1753, month: 1, day: 1);
                    var lastRecord = await _dwCustomerRepository.GetLastRecordByRegDateTime();
                    var totalRegistros = await _dwCustomerRepository.GetTotalRecords();
                    if(totalRegistros > 0 && lastRecord == null) {
                        _logger.LogWarning($"MigrarCustomerCommandHandler - Inconsistencia de datos, lastRecord fue null, pero existen registros en tabla");
                        return false;
                    }
                    if(lastRecord != null) {
                        lastDate = Convert.ToDateTime(lastRecord.RegDatetime);
                    }
                    var registros = await _customerRepository.GetByDate(lastDate, totalItems);
                    if(registros.Any()) {
                        var existentes = await _dwCustomerRepository.GetListByFilter(x => registros.Select(y => y.PlayerId).Contains(x.PlayerId));
                        var listaIds = existentes.Select(x => x.PlayerId);
                        var registrosMapear = registros.Where(x => !listaIds.Contains(x.PlayerId));

                        var registrosMapeados = _mapper.Map<List<DWCustomer>>(registrosMapear);


                        if(registrosMapeados.Any()) {
                            await _dwCustomerRepository.BulkInsert(registrosMapeados);
                            await _dwCustomerRepository.BulkSaveChanges();
                            continuarTarea = true;
                        }
                    }
                }
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarCustomerCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
        internal static async Task<DWCustomer?> GetLastRecord(IDWCustomerRepository _customerRepository) {
            var result = await _customerRepository.GetLastRecordByRegDateTime();
            return result;
        }
    }
}
