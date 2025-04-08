using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.AssociateCQ;
public class MigrarAssociateCommand : IRequest<bool> {
    public class MigrarAssociateCommandHandler : IRequestHandler<MigrarAssociateCommand, bool> {
        private readonly IAssociateRepository _associateRepository;
        private readonly IDWAssociateRepository _dwAssociateRepository;
        private readonly IMapper _mapper;
        public MigrarAssociateCommandHandler(IAssociateRepository repository, IAssociateRepository associateRepository, IDWAssociateRepository dwAssociateRepository, IMapper mapper) {
            _associateRepository = associateRepository;
            _dwAssociateRepository = dwAssociateRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(MigrarAssociateCommand request,CancellationToken cancellationToken) {
            var response = false;
            try {
                var registros = await _associateRepository.GetAll();
                var registrosMapeados = _mapper.Map<List<DWAssociate>>(registros);
                foreach (var registro in registrosMapeados) {
                    var insertado = _dwAssociateRepository.Add(registro);
                }

            } catch(Exception) {

                throw;
            }
            return false;
        }
    }

}
