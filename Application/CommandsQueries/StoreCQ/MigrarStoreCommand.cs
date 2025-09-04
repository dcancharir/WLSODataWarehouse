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

namespace Application.CommandsQueries.StoreCQ;
public class MigrarStoreCommand : IRequest<bool>{
    public class MigrarStoreCommandHandler : IRequestHandler<MigrarStoreCommand,bool> {
        private readonly IDWStoreRepository _dwStoreRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarStoreCommandHandler> _logger;
        public MigrarStoreCommandHandler(IDWStoreRepository dwStoreRepository, IStoreRepository storeRepository, IMapper mapper, ILogger<MigrarStoreCommandHandler> logger) {
            _dwStoreRepository = dwStoreRepository;
            _storeRepository = storeRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarStoreCommand request, CancellationToken cancellationToken) {
            bool response = false;
            try {
                var remoto = await _storeRepository.GetAll();
                var registros = _mapper.Map<List<DWStore>>(remoto);

                var idsStores = registros.Select(x => x.StoreId.ToLower().Trim());
                var registrosExistentes = await _dwStoreRepository.GetListByFilter(x => idsStores.Contains(x.StoreId.ToLower().Trim()));

                if(registrosExistentes.Any()) {
                    var idsExistentes = registrosExistentes.Select(x => x.StoreId.ToLower().Trim());
                    registros.RemoveAll(x => idsExistentes.Contains(x.StoreId.ToLower().Trim()));
                }

                if(registros.Count != 0) {
                    await _dwStoreRepository.BulkInsert(registros);
                    await _dwStoreRepository.SaveChanges();
                }
                //foreach(var item in registros) {
                //    Expression<Func<DWStore, bool>> predicate = c => c.StoreId == item.StoreId;
                //    await _dwStoreRepository.AddIfNotExist(item, predicate);
                //    await _dwStoreRepository.SaveChanges();
                //}
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarStoreCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
