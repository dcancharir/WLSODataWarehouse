using Application.IRepositories.DW;
using Application.IRepositories.MySql;
using AutoMapper;
using DWDomain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CommandsQueries.PromosCQ;
public class MigrarPromosCommand : IRequest<bool>{
    public class MigrarPromosCommandHandler : IRequestHandler<MigrarPromosCommand,bool>{
        private readonly IPromosRepository _promosRepository;
        private readonly IDWPromosRepository _dwPromosRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarPromosCommandHandler> _logger;
        public MigrarPromosCommandHandler(IPromosRepository promosRepository, IDWPromosRepository dwPromosRepository, IMapper mapper, ILogger<MigrarPromosCommandHandler> logger) {
            _promosRepository = promosRepository;
            _dwPromosRepository = dwPromosRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarPromosCommand request,CancellationToken cancellationToken) {
            bool response = false;
            try {
                var totalMysql = await _promosRepository.GetCountAll();
                var totalSql = await _dwPromosRepository.GetCountAll();
                if(totalMysql != totalSql) {
                    _logger.LogWarning($"MigrarProcessorCommandHandler - No se encontraton cambios en tablas");
                    return true;
                }

                var registros= await _promosRepository.GetAll();
                if(!registros.Any()) {
                    _logger.LogWarning($"MigrarPromosCommandHandler - No se encontraron registros a migrar");
                    return true;
                }
                var existentes = await _dwPromosRepository.GetListByFilter(x => registros.Select(y => y.PromoId).Contains(x.PromoId));
                var listaIds = existentes.Select(x => x.PromoId);

                var registrosMapear = registros.Where(x => !listaIds.Contains(x.PromoId));
                var registrosMapeados = _mapper.Map<List<DWPromos>>(registrosMapear);

                if(registrosMapeados.Any()) {
                    await _dwPromosRepository.BulkInsert(registrosMapeados);
                    await _dwPromosRepository.BulkSaveChanges();
                }
                response = true;
            } catch(Exception ex) {
                response = false;
                _logger.LogError($"MigrarPromosCommandHandler - {ex.Message}");
            }
            return response;
        }
    }
}
