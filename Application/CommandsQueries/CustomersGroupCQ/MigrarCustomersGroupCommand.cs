using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Logging;
using MySqlDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.CustomersGroupCQ;
public class MigrarCustomersGroupCommand :IRequest<bool>{
    public class MigrarCustomersGroupCommandHandler : IRequestHandler<MigrarCustomersGroupCommand, bool> {
        private readonly IDWCustomersGroupRepository _dwCustomersGroupRepository;
        private readonly ICustomersGroupRepository _customersGroupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarCustomersGroupCommandHandler> _logger;
        public MigrarCustomersGroupCommandHandler(IDWCustomersGroupRepository dwCustomersGroupRepository, ICustomersGroupRepository customersGroupRepository, IMapper mapper, ILogger<MigrarCustomersGroupCommandHandler> logger) {
            _dwCustomersGroupRepository = dwCustomersGroupRepository;
            _customersGroupRepository = customersGroupRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarCustomersGroupCommand request,CancellationToken cancellationToken) {
            bool response = false; 
            try {
                var registros = await _customersGroupRepository.GetAll();
                if(!registros.Any()) {
                    _logger.LogWarning($"MigrarCustomersGroupCommandHandler - No se encontraron registros a migrar");
                    return true;
                }
                var existentes = await _dwCustomersGroupRepository.GetListByFilter(x => registros.Select(y => y.PlayerId).Contains(x.PlayerId) && registros.Select(y => y.GroupId).Contains(x.GroupId));
                var listaPlayerId = existentes.Select(x => x.PlayerId);
                var listaGroupId = existentes.Select(x => x.GroupId);

                var registrosMapear = registros.Where(x => !listaPlayerId.Contains(x.PlayerId) && !listaGroupId.Contains(x.GroupId));

                var registrosMapeados = _mapper.Map<List<DWCustomersGroup>>(registrosMapear);

                if(registrosMapeados.Any()) {
                    await _dwCustomersGroupRepository.BulkInsert(registrosMapeados);
                    await _dwCustomersGroupRepository.BulkSaveChanges();
                }
                //foreach(var item in registros) {
                //    Expression<Func<DWCustomersGroup,bool>> predicate = c => c.PlayerId == item.PlayerId && c.GroupId == item.GroupId;
                //    await _dwCustomersGroupRepository.AddIfNotExist(item, predicate);
                //    await _dwCustomersGroupRepository.SaveChanges();
                //}
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarCustomersGroupCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
