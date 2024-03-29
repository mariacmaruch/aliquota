﻿using Aliquota.Domain.Dto;
using Aliquota.Domain.Entities;
using Aliquota.Domain.Interfaces.Repositories;
using Aliquota.Domain.Interfaces.Services;
using Aliquota.Domain.Services.Exceptions;
using AutoMapper;

namespace Aliquota.Domain.Services.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;

        public ContaService(IContaRepository contaRepository, IMapper mapper)
        {
            _contaRepository = contaRepository;
            _mapper = mapper;
        }

        private void ValidateNumero(int numero)
        {
            if(!_contaRepository.GetNumerosConta().Contains(numero))
                throw new InvalidContaException("Número da conta não encontrada.");
        }

        public ContaDto Get(int id)
        {
            var conta = _contaRepository.Get(id);

            return _mapper.Map<ContaDto>(conta);
        }

        public ContaDto Depositar(ContaDto conta)
        {
            ValidateNumero(conta.Numero);

            var contaExistente = _contaRepository.GetByNumero(conta.Numero);

            var contaEntity = _mapper.Map<ContaEntity>(conta);
            contaEntity.Id = contaExistente.Id;
            contaEntity.Saldo += contaExistente.Saldo;

            var deposito = _contaRepository.Depositar(contaEntity);

            return _mapper.Map<ContaDto>(deposito);
        }
    }
}
