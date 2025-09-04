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

namespace Application.CommandsQueries.PaymentMethodCQ;
public class MigrarPaymentMethodCommand : IRequest<bool>{
    public class MigrarPaymentMethodCommandHandler : IRequestHandler<MigrarPaymentMethodCommand,bool>{
        private readonly IDWPaymentMethodRepository _dwPaymentMethodRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MigrarPaymentMethodCommand> _logger;
        public MigrarPaymentMethodCommandHandler(IDWPaymentMethodRepository dwPaymentMethodRepository, IPaymentMethodRepository paymentMethodRepository, IMapper mapper, ILogger<MigrarPaymentMethodCommand> logger) {
            _dwPaymentMethodRepository = dwPaymentMethodRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<bool> Handle(MigrarPaymentMethodCommand request,CancellationToken cancellationToken) {
            bool response = false;
            try {
                var remoto = await _paymentMethodRepository.GetAll();
                var registros = _mapper.Map<List<DWPaymentMethod>>(remoto);
                var registrosExistentes = await _dwPaymentMethodRepository.GetAll();

                foreach (var item in registrosExistentes)
                {
                    var exist = registros.Where(x => x.MethodId.ToLower().Trim() == item.MethodId.ToLower().Trim() && item.Type == item.Type).FirstOrDefault();
                    if(exist != null) {
                        registros.Remove(exist);
                    }
                }
                if(registros.Count != 0) {
                    await _dwPaymentMethodRepository.BulkInsert(registros);
                    await _dwPaymentMethodRepository.SaveChanges();
                }
                //foreach (var item in registros) {
                //    Expression<Func<DWPaymentMethod,bool>> predicate = c => c.MethodId == item.MethodId && c.Type == item.Type;
                //    await _dwPaymentMethodRepository.AddIfNotExist(item, predicate);
                //    await _dwPaymentMethodRepository.SaveChanges();
                //}
                response = true;
            } catch(Exception ex) {
                _logger.LogError($"MigrarPaymentMethodCommandHandler - {ex.Message}");
                response = false;
            }
            return response;
        }
    }
}
