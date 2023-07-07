using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Mappers.Products
{

    public class ProductMapperConfiguration : MapperConfiguration
    {
        public ProductMapperConfiguration(ProductMapper mapper) : base(mapper.Mapper)
        {

        }
    }
}
