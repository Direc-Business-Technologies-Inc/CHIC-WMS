using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Registers
{
    /*
     * 
     * 
			typeof(DocumentMapper),
            typeof(ServiceLayerEnumMapper)
     */
    public class AutoMapperRegisters : Profile
    {
        public AutoMapperRegisters()
        {
        }
    }

    public static class MapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllMembers<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expr)
        {
            var destinationType = typeof(TDestination);

            foreach (var property in destinationType.GetProperties())
                expr.ForMember(property.Name, opt => opt.Ignore());

            return expr;
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonUdfMembers<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expr)
        {
            var destinationType = typeof(TDestination);

            foreach (var property in destinationType.GetProperties())
                if (!property.Name.StartsWith("U_"))
                    expr.ForMember(property.Name, opt => opt.Ignore());

            return expr;
        }

    }
}
