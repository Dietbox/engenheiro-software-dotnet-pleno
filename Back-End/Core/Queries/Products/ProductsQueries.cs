using Dietbox.ECommerce.Core.DTO.Products;
using Dietbox.ECommerce.Core.Exceptions;
using Dietbox.ECommerce.Core.Interfaces.Products;
using Dietbox.ECommerce.Core.Mappers.Products;
using Dietbox.ECommerce.ORM.Entities.Products;
using Dietbox.ECommerce.ORM.Interfaces;
using Dietbox.ECommerce.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Queries.Products
{
    public class ProductsQueries : IProductsQueries
    {

        private readonly IRepository<Product> _repository;
        private readonly ITenant _tenant;
        private readonly ProductMapperConfiguration _mapper;

        public ProductsQueries(
            IRepository<Product> repository,
            ITenant tenant,
            ProductMapperConfiguration mapper
        )
        {
            _repository = repository;
            _tenant = tenant;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> Get()
        {
            Expression<Func<Product, bool>> expression = _tenant.Type == TenantType.Company ? (_ => _.CompanyID == _tenant.ID) : (_ => _.Active == true);
            List<Product> results = await _repository.Get(expression).ToListAsync();
            List<ProductDTO> products = _mapper.CreateMapper().Map<List<ProductDTO>>(results);
            return products;
        }

        public async Task<ProductDTO> Get(int id)
        {
            bool exists = await _repository.Get(_ => _.ID == id).AnyAsync();
            if (exists is false)
                throw new NotFoundException("O produto solicitado não foi encontrado.");

            Product result = await _repository.Get(_ => _.ID == id).FirstOrDefaultAsync();

            if (_tenant.Type is TenantType.Company && !(result.CompanyID == _tenant.ID))
                throw new InvalidParameterException("O produto solicitado não pode ser acessado pela entidade atual.");

            ProductDTO product = _mapper.CreateMapper().Map<ProductDTO>(result);
            return product;
        }
    }
}
