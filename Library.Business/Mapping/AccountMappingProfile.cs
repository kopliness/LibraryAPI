using AutoMapper;
using Library.Business.Dto;
using Library.DAL.Entities;

namespace Library.Business.Mapping;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<Account, AccountDto>()
            .ReverseMap();
    }
}