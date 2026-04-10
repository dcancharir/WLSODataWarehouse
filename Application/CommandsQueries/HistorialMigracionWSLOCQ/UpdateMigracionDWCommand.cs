using Application.IRepositories.DW;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.HistorialMigracionWSLOCQ;

public class UpdateMigracionDWCommand : IRequest<bool> {
    public string campo { get; set; }
    public DateTime fecha { get; set; }
    public class UpdateMigracionDWCommandHandler : IRequestHandler<UpdateMigracionDWCommand, bool> {
        private readonly IDWHistorialMigracionWSLORepository _repository;

        public UpdateMigracionDWCommandHandler(IDWHistorialMigracionWSLORepository repository) {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateMigracionDWCommand request, CancellationToken cancellationToken) {
            bool result = false;
            if(request.campo == "bonuses") {
                var model = await _repository.FirstOrDefault(x => x.bonuses == 0 && x.fechaoperacion == request.fecha.Date);
                if(model != null) {
                    await _repository.UpdateAndSave(model);
                    result = true;   
                }
            }
            if(request.campo == "bonusstatuslog") {
                var model = await _repository.FirstOrDefault(x => x.bonusstatuslog == 0 && x.fechaoperacion == request.fecha.Date);
                if(model != null) {
                    await _repository.UpdateAndSave(model);
                    result = true;
                }
            }
            if(request.campo == "customers") {
                var model = await _repository.FirstOrDefault(x => x.customers == 0 && x.fechaoperacion == request.fecha.Date);
                if(model != null) {
                    await _repository.UpdateAndSave(model);
                    result = true;
                }
            }
            if(request.campo == "realgameevents") {
                var model = await _repository.FirstOrDefault(x => x.realgameevents == 0 && x.fechaoperacion == request.fecha.Date);
                if(model != null) {
                    await _repository.UpdateAndSave(model);
                    result = true;
                }
            }
            return result;
            
        }
    }
}
