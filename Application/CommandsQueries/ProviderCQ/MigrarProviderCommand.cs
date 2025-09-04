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

namespace Application.CommandsQueries.ProviderCQ;
public class MigrarProviderCommand : IRequest<bool>{
    public class MigrarProviderCommandHandler : IRequestHandler<MigrarProviderCommand, bool> {
        private readonly IDWProviderRepository _dwProviderRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarProviderCommandHandler> _logger;
        public MigrarProviderCommandHandler(IDWProviderRepository dwProviderRepository, IProviderRepository providerRepository, IMapper mapper, ILogger<MigrarProviderCommandHandler> logger) {
            _dwProviderRepository = dwProviderRepository;
            _providerRepository = providerRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarProviderCommand request,CancellationToken cancellationToken) {
            bool response = false;
            try {
                var remoto = await _providerRepository.GetAll();
                var registros = _mapper.Map<List<DWProvider>>(remoto);
                var idsProvider = registros.Select(x => x.ProviderId);

                var registrosExistentes = await _dwProviderRepository.GetListByFilter(x => idsProvider.Contains(x.ProviderId));

                if(registrosExistentes.Any()) {
                    var idsExistentes = registrosExistentes.Select(x => x.ProviderId);
                    registros.RemoveAll(x=>idsExistentes.Contains(x.ProviderId));
                }
                if(registros.Count != 0) {
                    await _dwProviderRepository.BulkInsert(registros);
                    await _dwProviderRepository.SaveChanges();
                }
                //foreach (var registro in registros) {
                //    Expression<Func<DWProvider, bool>> predicate = c => c.ProviderId == registro.ProviderId;
                //    await _dwProviderRepository.AddIfNotExist(registro, predicate);
                //    await _dwProviderRepository.SaveChanges();
                //}
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarProviderCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
