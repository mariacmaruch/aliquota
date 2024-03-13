
using Aliquota.Domain.Dto;
using Aliquota.Domain.Entities;
using AutoMapper;

namespace Aliquota.Domain.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserDto, UserEntity>().ReverseMap().ForPath(u => u.Conta.Id, map => map.MapFrom(src => src.IdConta));
            CreateMap<ContaDto, ContaEntity>().ReverseMap();
            CreateMap<ProductDto, ProductEntity>().ReverseMap();
        }
    }
}
