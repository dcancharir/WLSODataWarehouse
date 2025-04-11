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
            var batchSize = 1000;
            DateTime lastDate = new DateTime(year:1753,month:1,day:1);
            try {
                var lastRecord = await _dwCustomerRepository.GetLastRecordByRegDateTime();
                if (lastRecord != null) {
                    lastDate = Convert.ToDateTime(lastRecord.RegDatetime);
                }
                var totalRecords = await _customerRepository.GetTotalRecordsByDate(lastDate);
                var batchCount = (totalRecords + batchSize - 1)/batchSize;
                for(int i = 1; i<= batchCount; i++) {
                    var startIndex = i ;
                    var batch = await _customerRepository.GetAllPaginated(startIndex, batchSize,lastDate);
                    var mapped = _mapper.Map<List<DWCustomer>>(batch);
                    foreach(var item in mapped) {
                        Expression<Func<DWCustomer, bool>> predicate = c => c.PlayerId == item.PlayerId;
                        await _dwCustomerRepository.AddIfNotExist(item, predicate);
                        await _dwCustomerRepository.SaveChanges();
                    }
                }
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarCustomerCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
        //public async Task<bool> Handle(MigrarCustomerCommand request,CancellationToken cancellationToken) {
        //    bool response = false;
        //    var batchSize = 1000;
        //    try {
        //        var lastRecord = await _dwCustomerRepository.GetLastRecordByRegDateTime();
        //        var totalRecords = 0;
        //        if(lastRecord == null) {
        //            //migrar todo
        //            totalRecords = await _customerRepository.GetTotalRecords();
        //        } else {

        //            totalRecords = await _customerRepository.GetTotalRecordsByDate(Convert.ToDateTime(lastRecord.RegDatetime));
        //        }
        //        var batchCount = (totalRecords + batchSize - 1) / batchSize;
        //        for (int i = 0; i<= batchCount; i++) {
        //            var startIndex = i*batchSize;
        //            var batch = lastRecord == null ? await _customerRepository.GetAllPaginated(startIndex,batchSize) : await _customerRepository.GetPaginatedByDate(startIndex,batchSize,Convert.ToDateTime(lastRecord.RegDatetime));
        //            var mapped = _mapper.Map<IEnumerable<DWCustomer>>(batch);
        //            foreach(var item in mapped) {
        //                Expression<Func<DWCustomer,bool>> predicate = c => c.PlayerId == item.PlayerId;
        //                await _dwCustomerRepository.AddIfNotExist(item,predicate);
        //                await _dwCustomerRepository.SaveChanges();
        //            }
        //        }
        //        response = true;
        //    } catch(Exception ex) {
        //        _logger.LogError($"MigrarCustomerCommandHandler - {ex.Message}");
        //        response = false;
        //    }
        //    return response;
        //}
    }
}
