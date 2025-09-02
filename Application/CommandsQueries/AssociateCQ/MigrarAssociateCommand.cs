using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.AssociateCQ;
public class MigrarAssociateCommand : IRequest<bool> {
    public class MigrarAssociateCommandHandler : IRequestHandler<MigrarAssociateCommand, bool> {
        private readonly IAssociateRepository _associateRepository;
        private readonly IDWAssociateRepository _dwAssociateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarAssociateCommandHandler> _logger;
        public MigrarAssociateCommandHandler(IAssociateRepository associateRepository, IDWAssociateRepository dwAssociateRepository, IMapper mapper, ILogger<MigrarAssociateCommandHandler> logger) {
            _associateRepository = associateRepository;
            _dwAssociateRepository = dwAssociateRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarAssociateCommand request,CancellationToken cancellationToken) {
            var response = false;
            try {
                var registros = await _associateRepository.GetAll();
              
                if(!registros.Any()) {
                    _logger.LogWarning($"MigrarAssociateCommandHandler - No se encontraron registros a migrar");
                    return true;
                }

                var existentes = await _dwAssociateRepository.GetListByFilter(x => registros.Select(y => y.Ruc).Contains(x.Ruc));
               
                var listaRucs = existentes.Select(x => x.Ruc);
               
                var registrosMapear = registros.Where(x => !listaRucs.Contains(x.Ruc));

                var registrosMapeados = _mapper.Map<List<DWAssociate>>(registrosMapear);

                //var registros = registrosMapeados.RemoveAll(x => existentes.Select(y => y.Ruc).Contains(x.Ruc));
                if(registrosMapeados.Any() ) {
                    await _dwAssociateRepository.BulkInsert(registrosMapeados);
                    await _dwAssociateRepository.BulkSaveChanges();
                }

                //foreach (var registro in registrosMapeados) {
                //    Expression<Func<DWAssociate,bool>> predicate = c => c.Ruc == registro.Ruc;
                //    a10wAssociateRepository.AddIfNotExist(registro,predicate);
                //    await _dwAssociateRepository.SaveChanges();
                //}
                response = true;

            } catch(Exception ex) {
                response = false;
                _logger.LogError($"MigrarAssociateCommandHandler - {ex.Message}");
            }
            return response;
        }
    }

}
