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

namespace Application.CommandsQueries.BonusesCQ;

public class MigrarBonusesCommand2 : IRequest<bool> {
    public DateTime fechaoperacion {  get; set; }
    public class MigrarBonusesCommandHandler2 : IRequestHandler<MigrarBonusesCommand, bool> {
        private readonly IDWBonusesRepository _dwBonusesRepository;
        private readonly IBonusesRepository _bonusesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarBonusesCommand2> _logger;
        private readonly IConfiguration _configuration;
        private int LimitePorPaginacion;
        public MigrarBonusesCommandHandler2(IDWBonusesRepository dwBonusesRepository, IBonusesRepository bonusesRepository, IMapper mapper, ILogger<MigrarBonusesCommand2> logger, IConfiguration configuration) {
            _dwBonusesRepository = dwBonusesRepository;
            _bonusesRepository = bonusesRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            LimitePorPaginacion = Convert.ToInt32(_configuration.GetSection("Variables")["LimitePorPaginacion"]);
        }
        public async Task<bool> Handle(MigrarBonusesCommand request, CancellationToken cancellationToken) {
            bool response = false;
            var batchSize = LimitePorPaginacion;
            long lastTimestamp = 0;
            try {
             
            } catch(Exception ex) {
                response = false;
                _logger.LogError($"MigrarBonusesCommandHandler - {ex.Message}");
            }
            return response;
        }
    }
}
