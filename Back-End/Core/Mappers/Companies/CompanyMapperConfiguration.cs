using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Mappers.Companies
{
    public class CompanyMapperConfiguration : MapperConfiguration
    {
        public CompanyMapperConfiguration(CompanyMapper mapper) : base(mapper.Mapper)
        {

        }
    }
}
