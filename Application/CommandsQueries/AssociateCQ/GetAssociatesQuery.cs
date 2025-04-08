using Application.IRepositories.MySql;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlDomain;

namespace Application.CommandsQueries.AssociateCQ;
public class GetAssociatesQuery : IRequest<IEnumerable<Associate>>{
    public int quantity {  get; set; }
    public class GetAssocistesQueryHandler : IRequestHandler<GetAssociatesQuery, IEnumerable<Associate>> {
        private readonly IAssociateRepository _repository;
        private readonly ILogger<GetAssocistesQueryHandler> _logger;
        public GetAssocistesQueryHandler(IAssociateRepository repository, ILogger<GetAssocistesQueryHandler> logger) {
            _repository = repository;
            _logger = logger;
        }
        public async Task<IEnumerable<Associate>> Handle(GetAssociatesQuery request, CancellationToken cancellationToken) {
            IEnumerable<Associate> result = Enumerable.Empty<Associate>();
            try {
                string[] properties = [];
                result = await _repository.GetAll();
            } catch(Exception ex) {
                _logger.LogError($"GetAssocistesQueryHandler ERROR - {ex.Message}");
            }
            return result;
        }
    }
}
