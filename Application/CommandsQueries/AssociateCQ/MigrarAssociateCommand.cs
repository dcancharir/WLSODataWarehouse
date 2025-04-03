using Application.IRepositories.MySql;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.AssociateCQ;
public class MigrarAssociateCommand : IRequest<bool> {
    public class MigrarAssociateCommandHandler : IRequestHandler<MigrarAssociateCommand, bool> {
        private readonly IAssociateRepository _repository;

        public MigrarAssociateCommandHandler(IAssociateRepository repository) {
            _repository = repository;
        }
        public async Task<bool> Handle(MigrarAssociateCommand request,CancellationToken cancellationToken) {
            var response = false;
            try {

            } catch(Exception) {

                throw;
            }
            return false;
        }
    }

}
