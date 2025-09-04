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

namespace Application.CommandsQueries.ProcessorCQ;
public class MigrarProcessorCommand : IRequest<bool>{
    public class MigrarProcessorCommandHandler : IRequestHandler<MigrarProcessorCommand,bool> {
        private readonly IDWProcessorRepository _dwProcessorRepository;
        private readonly IProcessorRepository _processorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarProcessorCommandHandler> _logger;
        public MigrarProcessorCommandHandler(IDWProcessorRepository dwProcessorRepository, IProcessorRepository processorRepository, IMapper mapper, ILogger<MigrarProcessorCommandHandler> logger) {
            _dwProcessorRepository = dwProcessorRepository;
            _processorRepository = processorRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarProcessorCommand request,CancellationToken cancellationToken) {
            bool response = false;
            try {
                var remoto = await _processorRepository.GetAll();
                var registros = _mapper.Map<List<DWProcessor>>(remoto);

                var idsProcessor = registros.Select(x => x.ProcessorId);
                var registrosExistentes = await _dwProcessorRepository.GetListByFilter(x => idsProcessor.Contains(x.ProcessorId));

                if(registrosExistentes.Any()) {
                    var idsExistentes = registrosExistentes.Select(x => x.ProcessorId);
                    registros.RemoveAll(x => idsExistentes.Contains(x.ProcessorId));
                }
                if(registros.Count != 0) {
                    await _dwProcessorRepository.BulkInsert(registros);
                    await _dwProcessorRepository.SaveChanges();
                }
                //foreach (var registro in registros) {
                //    Expression<Func<DWProcessor,bool>> predicate = c => c.ProcessorId == registro.ProcessorId;
                //    await _dwProcessorRepository.AddIfNotExist(registro, predicate);
                //    await _dwProcessorRepository.SaveChanges();
                //}
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarProcessorCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }

    }
}
