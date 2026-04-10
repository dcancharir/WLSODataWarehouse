using Application.IRepositories.DW;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using static Application.CommandsQueries.CustomerCQ.GetListCustomersQuery;

namespace Application.CommandsQueries.HistorialMigracionWSLOCQ;

public class GetLastRecordHistorialMigracionDWQuery : IRequest<DWHistorialMigracionWSLOViewModel?> {
    public class GetLastRecordHistorialMigracionDWQueryHandler : IRequestHandler<GetLastRecordHistorialMigracionDWQuery, DWHistorialMigracionWSLOViewModel?> {
        private readonly ILogger<GetListCustomersQueryHandler> _logger;
        private readonly IDWHistorialMigracionWSLORepository _repository;
        private readonly IMapper _mapper;
        public GetLastRecordHistorialMigracionDWQueryHandler(ILogger<GetListCustomersQueryHandler> logger, IDWHistorialMigracionWSLORepository repository, IMapper mapper) {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<DWHistorialMigracionWSLOViewModel?> Handle(GetLastRecordHistorialMigracionDWQuery request, CancellationToken cancellationToken) {
            var ultimoRegistro =await  _repository.GetLastRecord();
            if(ultimoRegistro == null) {
                return null;
            }
            return _mapper.Map<DWHistorialMigracionWSLOViewModel>(ultimoRegistro);

        }
    }
}
