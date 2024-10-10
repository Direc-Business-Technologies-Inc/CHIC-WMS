using AutoMapper;
using DataManager.Models.Bins;
using DataManager.Models.Receiving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataManager.Models.APIs.BinMappingAPIModel;

namespace DataManager.Models.Registers
{
	public class AutoMapperRegisters : Profile
	{
		public AutoMapperRegisters()
		{
			CreateMap<BinMappingHeaders, BinMapping>()
				.ForMember(x => x.CreateDate, a => a.MapFrom(z => (z.CreateDate == null) ? DateTime.Now : DateTime.Now))
				.ForMember(x => x.ImageName, a => a.MapFrom(z => z.FileName))
				.ForMember(dest => dest.BinMappingPins, opt => opt.MapFrom(src => src.BinMappingPins));

			CreateMap<BinMapping, BinMappingHeaders>()
				.ForMember(x => x.FileName, a => a.MapFrom(z => z.ImageName))
				.ForMember(dest => dest.BinMappingPins, opt => opt.MapFrom(src => src.BinMappingPins));

			CreateMap<BinMappingPins, BinMappingPin>();
			CreateMap<BinMappingPin, BinMappingPins>();
        }
	}
}