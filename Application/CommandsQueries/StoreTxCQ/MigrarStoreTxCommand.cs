﻿using Application.IRepositories.DW;
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

namespace Application.CommandsQueries.StoreTxCQ;
public class MigrarStoreTxCommand : IRequest<bool>{
    public class MigrarStoreTxCommandHandler : IRequestHandler<MigrarStoreTxCommand, bool> {
        private readonly IDWStoreTxRepository _dwStoreTxRepository;
        private readonly IStoreTxRepository _storeTxRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarStoreTxCommandHandler> _logger;
        public MigrarStoreTxCommandHandler(IDWStoreTxRepository dwStoreTxRepository, IStoreTxRepository storeTxRepository, IMapper mapper, ILogger<MigrarStoreTxCommandHandler> logger) {
            _dwStoreTxRepository = dwStoreTxRepository;
            _storeTxRepository = storeTxRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(MigrarStoreTxCommand request, CancellationToken cancellationToken) {
            bool response;
            try {
                var remoto = await _storeTxRepository.GetAll();
                var registros = _mapper.Map<List<DWStoreTx>>(remoto);
                foreach(var item in registros) {
                    Expression<Func<DWStoreTx,bool>> predicate = c => c.TxId == item.TxId;
                    await _dwStoreTxRepository.AddIfNotExist(item,predicate);
                    await _dwStoreTxRepository.SaveChanges();
                }
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarStoreTxCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
