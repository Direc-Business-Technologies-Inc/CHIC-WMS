using AutoMapper;
using db = Application.Libraries.SAP.DB.Models;
using sl = Application.Libraries.SAP.SL;
namespace Application.Libraries.Mappers;

public class ServiceTypeMapper : Profile
{
    public ServiceTypeMapper()
    {
        CreateMap<db.SERVICE_DATA, sl.SERVICE_DATA>();
        CreateMap<db.SERVICE_DATA, sl.SERVICE_DATASingle>();
        CreateMap<db.SERVICE_DATA_ROW, sl.SERVICE_DATA_ROW>();
        CreateMap<db.SERVICE_TYPE, sl.U_SERVICE_TYPE>();
        CreateMap<db.SERVICE_TYPE, sl.U_SERVICE_TYPESingle>();
    }
}
