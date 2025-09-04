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

namespace Application.CommandsQueries.BrandCQ;
public class MigrarBrandCommand :IRequest<bool>{
    public class MigrarBrandCommandHandler : IRequestHandler<MigrarBrandCommand, bool> {
        private readonly IBrandRepository _brandRepository;
        private readonly IDWBrandRepository _dwBrandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarBrandCommandHandler> _logger;
        public MigrarBrandCommandHandler(IBrandRepository brandRepository, IDWBrandRepository dwBrandRepository, IMapper mapper, ILogger<MigrarBrandCommandHandler> logger) {
            _brandRepository = brandRepository;
            _dwBrandRepository = dwBrandRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarBrandCommand request,CancellationToken cancellationToken) {
            bool response = false;
            try {
                var registros = await _brandRepository.GetAll();
                if(!registros.Any()) {
                    _logger.LogWarning($"MigrarBrandCommandHandler - No se encontraron registros a migrar");
                    return true;
                }
                var mapped = _mapper.Map<List<DWBrand>>(registros);
                var idsBrands = mapped.Select(x => x.BrandId.ToLower().Trim());

                var existentes = await _dwBrandRepository.GetListByFilter(x => idsBrands.Contains(x.BrandId.ToLower().Trim()));

                if(existentes.Any()) {
                    var idsExistentes = existentes.Select(x => x.BrandId.ToLower().Trim());
                    mapped.RemoveAll(x => idsExistentes.Contains(x.BrandId.ToLower().Trim()));
                }
                if(mapped.Any()) {
                    await _dwBrandRepository.BulkInsert(mapped);
                    await _dwBrandRepository.BulkSaveChanges();
                }
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarBrandCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
