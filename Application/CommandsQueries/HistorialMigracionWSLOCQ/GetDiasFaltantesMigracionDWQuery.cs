using Application.IRepositories.DW;
using Application.ViewModels;
using AutoMapper;
using DWDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.HistorialMigracionWSLOCQ;

public class GetDiasFaltantesMigracionDWQuery : IRequest<IEnumerable<DWHistorialMigracionWSLOViewModel>?> {
    public string campo { get; set; }
    public class GetDiasFaltantesMigracionDWQueryHandler : IRequestHandler<GetDiasFaltantesMigracionDWQuery, IEnumerable<DWHistorialMigracionWSLOViewModel>?> {
        private readonly IMapper _mapper;
        private readonly IDWHistorialMigracionWSLORepository _repo;
        public GetDiasFaltantesMigracionDWQueryHandler(IMapper mapper, IDWHistorialMigracionWSLORepository repo) {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<IEnumerable<DWHistorialMigracionWSLOViewModel>?> Handle(GetDiasFaltantesMigracionDWQuery request, CancellationToken cancellationToken) {
            IEnumerable<DWHistorialMigracionWSLO>? resultDb = null;
            if(request == null) {
                return null;
            }
            if(request.campo == "bonuses") {
                resultDb = await _repo.GetQuery(null, x => x.bonuses == 0);
            }
            if(request.campo == "bonusstatuslog") {
                resultDb = await _repo.GetQuery(null, x => x.bonusstatuslog == 0);
            }
            if(request.campo == "customers") {
                resultDb = await _repo.GetQuery(null, x => x.customers == 0);
            }
            if(request.campo == "realgameevents") {
                resultDb = await _repo.GetQuery(null, x => x.realgameevents == 0);
            }
            if(resultDb == null) {
                return null;
            }
            return _mapper.Map<IEnumerable<DWHistorialMigracionWSLOViewModel>>(resultDb);
        }
    }
}
