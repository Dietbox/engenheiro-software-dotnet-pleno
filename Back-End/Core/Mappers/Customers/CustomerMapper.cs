using AutoMapper;
using Dietbox.ECommerce.Core.Commands.Companies;
using Dietbox.ECommerce.Core.Commands.Users;
using Dietbox.ECommerce.Core.DTO.Companies;
using Dietbox.ECommerce.Core.DTO.Customers;
using Dietbox.ECommerce.Core.Utils;
using Dietbox.ECommerce.ORM.Entities.Companies;
using Dietbox.ECommerce.ORM.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Mappers.Customers
{
    public class CustomerMapper
    {

        public void Mapper(IMapperConfigurationExpression mapper)
        {
            CreateCustomerAccountCommandToEntity(mapper);
            EntityToDTO(mapper);
        }

        void CreateCustomerAccountCommandToEntity(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<CreateCustomerAccountCommand, Customer>()
                    .ForMember(dest => dest.Name, map => map.MapFrom(source => source.Name))
                    .ForMember(dest => dest.Email, map => map.MapFrom(source => source.Email))
                    .AfterMap((source, dest) =>
                    {
                        dest.Password = dest.Password.ToHash256();
                        dest.Active = true;
                    });
        }

        void EntityToDTO(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Customer, CustomerDTO>()
                    .ForMember(dest => dest.ID, map => map.MapFrom(source => source.ID))
                    .ForMember(dest => dest.Name, map => map.MapFrom(source => source.Name))
                    .ForMember(dest => dest.Email, map => map.MapFrom(source => source.Email))
                    .ForMember(dest => dest.CreatedDate, map => map.MapFrom(source => source.CreatedDate))
                    .AfterMap((source, dest) =>
                    {

                    });
        }


    }
}
