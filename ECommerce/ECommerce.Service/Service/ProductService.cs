using ECommerce.Domain.Dto;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Infra;
using ECommerce.Domain.Interfaces.Repository;
using ECommerce.Domain.Interfaces.Services;
using ECommerce.Domain.Models;

namespace ECommerce.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Company> _companyRepository;
        private readonly IAccessManager _accessManager;

        public ProductService(IGenericRepository<Product> productRepository, IAccessManager accessManager, IGenericRepository<Company> companyRepository)
        {
            _productRepository = productRepository;
            _accessManager = accessManager;
            _companyRepository = companyRepository;
        }

        public async Task CreateProduct(ProductInputModel product, string userEmail)
        {
            var companyUser = await _accessManager.GetUser(userEmail);

            if (companyUser is not null) 
            {
                var company = await _companyRepository
                    .FindAsync(x => x.ApplicationUser.Id == companyUser.Id);

                var newProduct = new Product(product.Description, product.BarCode, company.FirstOrDefault());

                await _productRepository.AddAsync(newProduct);

                await _productRepository.SaveAsync();
            }
        }

        public async Task<IEnumerable<ProductDto>> ListProductsByCompany(string userEmail)
        {
            var companyUser = await _accessManager.GetUser(userEmail);

            if (companyUser is not null)
            {
                var company = await _companyRepository
                    .FindAsync(x => x.ApplicationUser.Id == companyUser.Id);

                var products = await _productRepository
                    .FindAsync(x => x.Company.Id == company.FirstOrDefault().Id);

                return products.Select(x => new ProductDto()
                {
                    Id = x.Id,
                    BarCode = x.BarCode,
                    Description = x.Description
                });
            }

            return Enumerable.Empty<ProductDto>();
        }

        public async Task<IEnumerable<ProductDto>> ListAllProducts()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(x => new ProductDto()
            {
                Id = x.Id,
                BarCode = x.BarCode,
                Description = x.Description
            });
        }
    }
}
