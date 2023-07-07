using AutoMapper;
using Dietbox.ECommerce.Core.Commands.Companies;
using Dietbox.ECommerce.Core.Commands.Products;
using Dietbox.ECommerce.Core.DTO.Companies;
using Dietbox.ECommerce.Core.DTO.Products;
using Dietbox.ECommerce.Core.Utils;
using Dietbox.ECommerce.ORM.Entities.Companies;
using Dietbox.ECommerce.ORM.Entities.Products;
using Dietbox.ECommerce.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Mappers.Products
{
    public class ProductMapper
    {
        private readonly ITenant _tenant;
        public ProductMapper(ITenant tenant)
        {
            _tenant = tenant;
        }

        public void Mapper(IMapperConfigurationExpression mapper)
        {
            CreateProductCommandToEntity(mapper);
            EntityToDTO(mapper);
        }

        void CreateProductCommandToEntity(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<CreateProductCommand, Product>()
                    .ForMember(dest => dest.Name, map => map.MapFrom(source => source.Name))
                    .ForMember(dest => dest.Description, map => map.MapFrom(source => source.Description))
                    .ForMember(dest => dest.Brand, map => map.MapFrom(source => source.Brand))
                    .ForMember(dest => dest.Price, map => map.MapFrom(source => source.Price))
                    .ForMember(dest => dest.Code, map => map.MapFrom(source => source.Code))
                    .ForMember(dest => dest.Stock, map => map.MapFrom(source => source.Stock))
                    .AfterMap((source, dest) =>
                    {
                        dest.CompanyID = _tenant.ID;
                        dest.Active = true;
                    });
        }

        void EntityToDTO(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Product, ProductDTO>()
                    .ForMember(dest => dest.ID, map => map.MapFrom(source => source.ID))
                    .ForMember(dest => dest.CompanyID, map => map.MapFrom(source => source.CompanyID))
                    .ForMember(dest => dest.Name, map => map.MapFrom(source => source.Name))
                    .ForMember(dest => dest.Description, map => map.MapFrom(source => source.Description))
                    .ForMember(dest => dest.Brand, map => map.MapFrom(source => source.Brand))
                    .ForMember(dest => dest.Price, map => map.MapFrom(source => source.Price))
                    .ForMember(dest => dest.Code, map => map.MapFrom(source => source.Code))
                    .ForMember(dest => dest.Stock, map => map.MapFrom(source => source.Stock))
                    .ForMember(dest => dest.Active, map => map.MapFrom(source => source.Active))
                    .ForMember(dest => dest.CreatedDate, map => map.MapFrom(source => source.CreatedDate))
                    .AfterMap((source, dest) =>
                    {

                    });
        }
    }
}
