using AutoMapper;
using Dietbox.ECommerce.Core.Commands.Companies;
using Dietbox.ECommerce.Core.DTO.Companies;
using Dietbox.ECommerce.Core.Utils;
using Dietbox.ECommerce.ORM.Entities.Companies;
using Dietbox.ECommerce.ORM.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Mappers.Companies
{
    public class CompanyMapper
    {

        public void Mapper(IMapperConfigurationExpression mapper)
        {
            CreateCompanyCommandToEntity(mapper);
            EntityToDTO(mapper);
        }

        void CreateCompanyCommandToEntity(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<CreateCompanyAccountCommand, Company>()
                    .ForMember(dest => dest.Name, map => map.MapFrom(source => source.Name))
                    .ForMember(dest => dest.CNPJ, map => map.MapFrom(source => source.CNPJ))
                    .ForMember(dest => dest.Email, map => map.MapFrom(source => source.Email))
                    .AfterMap((source, dest) =>
                    {
                        dest.Password = dest.Password.ToHash256();
                    });
        }


        void EntityToDTO(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Company, CompanyDTO>()
                    .ForMember(dest => dest.ID, map => map.MapFrom(source => source.ID))
                    .ForMember(dest => dest.Name, map => map.MapFrom(source => source.Name))
                    .ForMember(dest => dest.CNPJ, map => map.MapFrom(source => source.CNPJ))
                    .ForMember(dest => dest.Email, map => map.MapFrom(source => source.Email))
                    .ForMember(dest => dest.CreatedDate, map => map.MapFrom(source => source.CreatedDate))
                    .AfterMap((source, dest) =>
                    {

                    });
        }
    }
}
