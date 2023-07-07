using AutoMapper;
using Dietbox.ECommerce.Core.Mappers.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Mappers.Customers
{

    public class CustomerMapperConfiguration : MapperConfiguration
    {
        public CustomerMapperConfiguration(CustomerMapper mapper) : base(mapper.Mapper)
        {

        }
    }
}
