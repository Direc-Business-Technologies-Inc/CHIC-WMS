using Application.Libraries.Registers;
using Application.Libraries.SAP.DB.Models;
using Application.Libraries.SAP.SL;
using AutoMapper;

namespace Application.Libraries.Mappers
{
    public class BatchNumberMapper : Profile
    {
        public BatchNumberMapper()
        {

            CreateMap<OBTN, BatchNumberDetail>()
                .IgnoreAllNonUdfMembers()
                .ForMember(x => x.DocEntry,
                    o => o.MapFrom(s => s.AbsEntry))
                .ForMember(d => d.ItemCode,
                    o => o.MapFrom(s => s.ItemCode))
           .ForMember(d => d.Status,
                o => o.MapFrom(s => s.Status))
           .ForMember(d => d.Batch,
                o => o.MapFrom(s => s.DistNumber))
           .ForMember(d => d.BatchAttribute1,
                o => o.MapFrom(s => s.MnfSerial))
           .ForMember(d => d.BatchAttribute2,
                o => o.MapFrom(s => s.LotNumber))
           .ForMember(d => d.AdmissionDate,
                o => o.MapFrom(s => s.InDate))
           .ForMember(d => d.ManufacturingDate,
                o => o.MapFrom(s => s.MnfDate))
           .ForMember(d => d.ExpirationDate,
                o => o.MapFrom(s => s.ExpDate))
           .ForMember(d => d.Details,
                o => o.MapFrom(s => s.Notes))
           .ForMember(d => d.SystemNumber,
                o => o.MapFrom(s => s.SysNumber));


            CreateMap<OBTN, BatchNumber>()
                .IgnoreAllNonUdfMembers()
                .ForMember(d => d.ItemCode,
                    o => o.MapFrom(s => s.ItemCode))
           .ForMember(d => d.BatchNumberProperty,
                o => o.MapFrom(s => s.DistNumber))
           .ForMember(d => d.ManufacturerSerialNumber,
                o => o.MapFrom(s => s.MnfSerial))
           .ForMember(d => d.InternalSerialNumber,
                o => o.MapFrom(s => s.LotNumber))
           .ForMember(d => d.AddmisionDate,
                o => o.MapFrom(s => s.InDate))
           .ForMember(d => d.ManufacturingDate,
                o => o.MapFrom(s => s.MnfDate))
           .ForMember(d => d.ExpiryDate,
                o => o.MapFrom(s => s.ExpDate))
           .ForMember(d => d.Notes,
                o => o.MapFrom(s => s.Notes))
           .ForMember(d => d.SystemSerialNumber,
                o => o.MapFrom(s => s.SysNumber));



            CreateMap<BatchNumberDetail, BatchNumber>()
                .IgnoreAllNonUdfMembers()
                .ForMember(d => d.ItemCode,
                    o => o.MapFrom(s => s.ItemCode))
           .ForMember(d => d.BatchNumberProperty,
                o => o.MapFrom(s => s.Batch))
           .ForMember(d => d.ManufacturerSerialNumber,
                o => o.MapFrom(s => s.BatchAttribute1))
           .ForMember(d => d.InternalSerialNumber,
                o => o.MapFrom(s => s.BatchAttribute2))
           .ForMember(d => d.AddmisionDate,
                o => o.MapFrom(s => s.AdmissionDate))
           .ForMember(d => d.ManufacturingDate,
                o => o.MapFrom(s => s.ManufacturingDate))
           .ForMember(d => d.ExpiryDate,
                o => o.MapFrom(s => s.ExpirationDate))
           .ForMember(d => d.Notes,
                o => o.MapFrom(s => s.Details))
           .ForMember(d => d.SystemSerialNumber,
                o => o.MapFrom(s => s.SystemNumber))
           .ReverseMap();

        }
    }
}
