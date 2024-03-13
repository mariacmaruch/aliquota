using Aliquota.Domain.Dto;
using Aliquota.Domain.Entities;
using Aliquota.Domain.Interfaces.Repositories;
using Aliquota.Domain.Interfaces.Services;
using Aliquota.Domain.Services.Exceptions;
using AutoMapper;

namespace Aliquota.Domain.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IContaRepository contaRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _contaRepository = contaRepository;
        }

        public UserDto Create(UserDto user)
        {
            var usuario = _mapper.Map<UserEntity>(user);

            var usuarioEntity = _userRepository.Create(usuario);

            return _mapper.Map<UserDto>(usuarioEntity);
        }

        public UserDto Update(int id, UserDto user)
        {
            ValidateId(id);

            ValidateNumero(user.Conta.Numero);

            var usuario = _userRepository.Get(id);

            if(usuario.Email != user.Email)
            {
                ValidateEmail(user.Email);
            }

            var usuarioEntity = _mapper.Map<UserEntity>(user);

            usuarioEntity.Id = id;

            var updated = _userRepository.Update(usuarioEntity);

            return _mapper.Map<UserDto>(updated);
        }

        private void ValidateId(int id)
        {
            if (_userRepository.Get(id) == null)
                throw new InvalidUserException("Usuário não encontrado");
        }

        private void ValidateEmail(string email)
        {
            if(_userRepository.GetEmail().Contains(email))
                throw new InvalidEmailException("Email já cadastrado");
        }
        
        private void ValidateNumero(int numero)
        {
            if (_contaRepository.GetNumerosConta().Contains(numero))
            {
                throw new InvalidEmailException("Número de conta já cadastrado");
            }
        }

        public UserDto Get(int id)
        {
            ValidateId(id);

            var usuario = _userRepository.Get(id);

            var usuarioDto = _mapper.Map<UserDto>(usuario);
            
            var conta = _contaRepository.Get(usuario.IdConta);
            usuarioDto.Conta = _mapper.Map<ContaDto>(conta);

            return usuarioDto;
        }

        public List<UserDto> GetAll()
        {
            var usuario = _userRepository.GetAll();
            var listaUsers = new List<UserDto>();

            foreach(var item in usuario)
            {
                var userDto = _mapper.Map<UserDto>(item);

                var conta = _contaRepository.Get(item.IdConta);
                userDto.Conta = _mapper.Map<ContaDto>(conta);

                listaUsers.Add(userDto);
            }

            return listaUsers;
        }
    }
}
