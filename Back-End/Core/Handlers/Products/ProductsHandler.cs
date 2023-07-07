using Dietbox.ECommerce.Core.Commands.Products;
using Dietbox.ECommerce.Core.DTO.Products;
using Dietbox.ECommerce.Core.Exceptions;
using Dietbox.ECommerce.Core.Interfaces;
using Dietbox.ECommerce.Core.Interfaces.Products;
using Dietbox.ECommerce.Core.Mappers.Products;
using Dietbox.ECommerce.Core.Validations.Products;
using Dietbox.ECommerce.ORM.Entities.Customers;
using Dietbox.ECommerce.ORM.Entities.Products;
using Dietbox.ECommerce.ORM.Interfaces;
using Dietbox.ECommerce.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Dietbox.ECommerce.Core.Handlers.Products
{
    public class ProductsHandler : IProductsHandler
    {

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly ICommandValidator _validator;
        private readonly ITenant _tenant;
        private readonly ProductMapperConfiguration _mapper;

        public ProductsHandler(
            IRepository<Product> productRepository,
            IRepository<Order> orderRepository,
            ICommandValidator validator,
            ITenant tenant,
            ProductMapperConfiguration mapper
        )
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _validator = validator;
            _tenant = tenant;
            _mapper = mapper;
        }

        public async Task Buy(BuyProductCommand command)
        {
            (bool isValid, List<string> messages) = await _validator.Validate<BuyProductValidator, BuyProductCommand>(command);
            if (isValid is false)
                throw new InvalidParameterException(messages.ToArray());

            Product? productEntity = await _productRepository.Get(_ => _.ID == command.ID).FirstOrDefaultAsync();
            productEntity.Stock -= command.Amount;
            await _productRepository.Update(productEntity);

            Order orderEntity = new()
            {
                ProductID = command.ID,
                UserID = _tenant.ID,
                Amount = command.Amount,
                UnitPrice = productEntity.Price,
                TotalPrice = decimal.Multiply(productEntity.Price, command.Amount)
            };

            await _orderRepository.Insert(orderEntity);
        }

        public async Task<ProductDTO> Create(CreateProductCommand command)
        {
            (bool isValid, List<string> messages) = await _validator.Validate<CreateProductValidator, CreateProductCommand>(command);
            if (isValid is false)
                throw new InvalidParameterException(messages.ToArray());

            Product entity = _mapper.CreateMapper().Map<Product>(command);
            await _productRepository.Insert(entity);

            ProductDTO product = _mapper.CreateMapper().Map<ProductDTO>(entity);
            return product;
        }

        public Task Delete(DeleteProductCommand command)
        {
            throw new NotImplementedException();
        }


    }
}
