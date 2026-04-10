using Application.IRepositories.DW;
using Application.ViewModels;
using AutoMapper;
using DWDomain;
using K4os.Compression.LZ4.Encoders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.HistorialMigracionWSLOCQ;

public class CrearHistorialMigracionDWCommand : IRequest<bool> {
    public List<DWHistorialMigracionWSLOViewModel> registro {  get; set; }
    public class CrearHistorialMigracionDWCommandHandler : IRequestHandler<CrearHistorialMigracionDWCommand, bool> {
        private readonly IMapper _mapper;
        private readonly IDWHistorialMigracionWSLORepository _repo;

        public CrearHistorialMigracionDWCommandHandler(IMapper mapper, IDWHistorialMigracionWSLORepository repo) {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<bool> Handle(CrearHistorialMigracionDWCommand request, CancellationToken cancellationToken) {
            var response = false;
            if(request.registro == null) {
                return response;
            }
            try {
                var models = _mapper.Map<List<DWHistorialMigracionWSLO>>(request.registro);
                await _repo.BulkInsert(models);
                await _repo.BulkSaveChanges();
                response = true;
            } catch(Exception) {

                throw;
            }
            return response;
        }
    }
}
