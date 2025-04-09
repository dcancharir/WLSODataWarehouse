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
        public MigrarAssociateCommandHandler(IAssociateRepository repository, IAssociateRepository associateRepository, IDWAssociateRepository dwAssociateRepository, IMapper mapper, ILogger<MigrarAssociateCommandHandler> logger) {
            _associateRepository = associateRepository;
            _dwAssociateRepository = dwAssociateRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarAssociateCommand request,CancellationToken cancellationToken) {
            var response = false;
            try {
                var registros = await _associateRepository.GetAll();
                //var registros = new List<DWAssociate>() { 
                //    new DWAssociate() {
                //        Ruc = "10473161520",
                //        Name = "Test",
                //        Status = 1
                //    },
                //     new DWAssociate() {
                //        Ruc = "10473161522",
                //        Name = "Test2",
                //        Status = 1
                //    },
                //};
                //var registroExistentes = await _dwAssociateRepository.GetAll();

                //var registrosInsertar = registros.Where(c2 => !registroExistentes.Any(c1 => c1.Ruc == c2.Ruc)).ToList();
                var registrosMapeados = _mapper.Map<List<DWAssociate>>(registros);
                foreach (var registro in registrosMapeados) {
                    Expression<Func<DWAssociate,bool>> predicate = c => c.Ruc == registro.Ruc;
                    await _dwAssociateRepository.AddIfNotExist(registro,predicate);
                    await _dwAssociateRepository.SaveChanges();
                }
                response = true;

            } catch(Exception ex) {
                response = false;
                _logger.LogError($"MigrarAssociateCommandHandler - {ex.Message}");
            }
            return response;
        }
    }

}
