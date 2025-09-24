using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.UsersCQ;
public class MigrarUsersCommand : IRequest<bool>{
    public class MigrarUsersCommandHandler : IRequestHandler<MigrarUsersCommand, bool> {
        private readonly IDWUserRepository _dwUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<MigrarUsersCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private int LimitePorPaginacion;
        public MigrarUsersCommandHandler(IDWUserRepository dwUserRepository, IUserRepository userRepository, ILogger<MigrarUsersCommandHandler> logger, IMapper mapper, IConfiguration configuration) {
            _dwUserRepository = dwUserRepository;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            LimitePorPaginacion = Convert.ToInt32(_configuration.GetSection("Variables")["LimitePorPaginacion"]);
        }
        public async Task<bool> Handle(MigrarUsersCommand request,CancellationToken cancellationToken) {
            bool response;
            var batchSize = LimitePorPaginacion;
            uint lastId = 0;
            try {
                var lastRecord = await _dwUserRepository.GetLastRecord();
                if(lastRecord != null) {
                    lastId = lastRecord.UserId;
                }
                var totalRecords = await _userRepository.GetTotalRecordsById(lastId);
                var batchCount = (totalRecords + batchSize - 1) / batchSize;
                for(int i=0;i<=batchCount; i++) {
                    var startIndex = i*batchSize;
                    var batch = await _userRepository.GetPaginatedById(startIndex, batchSize, lastId);

                    var mapped = _mapper.Map<List<DWUser>>(batch);
                    var ids = mapped.Select(x => x.UserId);
                    var exists = await _dwUserRepository.GetListByFilter(x => ids.Contains(x.UserId));

                    if(exists.Any()) {
                        var idsExists = exists.Select(x => x.UserId).ToList();
                        mapped.RemoveAll(x=>idsExists.Contains(x.UserId));
                    }
                    if(mapped.Count > 0) {
                        await _dwUserRepository.BulkInsert(mapped);
                        await _dwUserRepository.BulkSaveChanges();
                    }
                }
            } catch(Exception) {

                throw;
            }
            return false;
        }
    }
}
