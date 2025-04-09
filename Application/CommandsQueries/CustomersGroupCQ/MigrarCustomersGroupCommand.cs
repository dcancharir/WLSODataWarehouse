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
                var customersGroup = await _customersGroupRepository.GetAll();
                var registros = _mapper.Map<List<DWCustomersGroup>>(customersGroup);
                foreach(var item in registros) {
                    Expression<Func<DWCustomersGroup,bool>> predicate = c => c.PlayerId == item.PlayerId && c.GroupId == item.GroupId;
                    await _dwCustomersGroupRepository.AddIfNotExist(item, predicate);
                    await _dwCustomersGroupRepository.SaveChanges();
                }
            } catch(Exception ex) {
                _logger.LogError($"MigrarCustomersGroupCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
