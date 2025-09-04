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

namespace Application.CommandsQueries.GroupsxCQ;
public class MigrarGroupsxCommand : IRequest<bool>{
    public class MigrarGroupsxCommandHandler : IRequestHandler<MigrarGroupsxCommand, bool> {
        private readonly IDWGroupsxRepository _dwGroupsxRepository;
        private readonly IGroupsxRepository _groupsxRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarGroupsxCommandHandler> _logger;
        public MigrarGroupsxCommandHandler(IDWGroupsxRepository dwGroupsxRepository, IGroupsxRepository groupsxRepository, IMapper mapper, ILogger<MigrarGroupsxCommandHandler> logger) {
            _dwGroupsxRepository = dwGroupsxRepository;
            _groupsxRepository = groupsxRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarGroupsxCommand request,CancellationToken cancellationToken) {
            bool response = false;
            try {
                var remoto = await _groupsxRepository.GetAll();
                var registros = _mapper.Map<List<DWGroupsx>>(remoto);
                var groupsIds = registros.Select(x => x.GroupId);
                var exists = await _dwGroupsxRepository.GetListByFilter(x => groupsIds.Contains(x.GroupId));
                if(exists.Any()) {
                    var idsExists = exists.Select(x=>x.GroupId);
                    registros.RemoveAll(x => idsExists.Contains(x.GroupId));
                }
                if(registros.Any()) {
                    await _dwGroupsxRepository.BulkInsert(registros);
                    await _dwGroupsxRepository.SaveChanges();
                }
                //foreach(var item in registros) {
                //    Expression<Func<DWGroupsx,bool>> predicate = c => c.GroupId == item.GroupId;
                //    await _dwGroupsxRepository.AddIfNotExist(item, predicate);
                //    await _dwGroupsxRepository.SaveChanges();
                //}
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarGroupsxCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
