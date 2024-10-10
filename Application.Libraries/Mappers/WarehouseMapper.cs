using Application.Libraries.SAP.DB.Models;
using Application.Models;
using AutoMapper;

namespace Application.Libraries.Mappers;

public class WarehouseMapper : Profile
{
    public WarehouseMapper()
    {
        CreateMap<OWHS, Warehouse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.WhsCode))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.WhsName))
            .ReverseMap();
    }
}
