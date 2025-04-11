using Application.IRepositories.MySql;
using MediatR;
using Microsoft.Extensions.Logging;
using MySqlDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.CustomerCQ;
public class GetListCustomersQuery :IRequest<IEnumerable<Customer>>{
    public int page { get; set; }
    public int pageSize { get; set; }
    public class GetListCustomersQueryHandler : IRequestHandler<GetListCustomersQuery, IEnumerable<Customer>> {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<GetListCustomersQueryHandler> _logger;
        public GetListCustomersQueryHandler(ICustomerRepository customerRepository, ILogger<GetListCustomersQueryHandler> logger) {
            _customerRepository = customerRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<Customer>>Handle(GetListCustomersQuery request,CancellationToken cancellationToken) {
            IEnumerable<Customer> result = Enumerable.Empty<Customer>();
            try {
                result = await _customerRepository.GetAll();
            } catch(Exception ex) {
                _logger.LogError($"GetListCustomersQueryHandler ERROR - {ex.Message}");
            }
            return result;
        }
    } 
}
