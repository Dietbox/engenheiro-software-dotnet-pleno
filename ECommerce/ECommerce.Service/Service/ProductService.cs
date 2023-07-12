using ECommerce.Domain.Dto;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Infra;
using ECommerce.Domain.Interfaces.Repository;
using ECommerce.Domain.Interfaces.Services;
using ECommerce.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Company> _companyRepository;
        private readonly IAccessManager _accessManager;
        private readonly IMemoryCache _cache;
        private const string productListCacheKey = "productList";

        public ProductService(IGenericRepository<Product> productRepository, 
            IAccessManager accessManager, IGenericRepository<Company> companyRepository, 
            IMemoryCache cache)
        {
            _productRepository = productRepository;
            _accessManager = accessManager;
            _companyRepository = companyRepository;
            _cache = cache;
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

                AddProductOnCache(newProduct);
            }
        }

        public async Task<IEnumerable<ProductDto>> ListProductsByCompany(string userEmail)
        {
            var companyUser = await _accessManager.GetUser(userEmail);

            if (companyUser is not null)
            {
                var company = await _companyRepository
                    .FindAsync(x => x.ApplicationUser.Id == companyUser.Id);

                var products = GetProductsFromCache();

                return products.Where(x => x.Company.Id == company.FirstOrDefault().Id)
                               .Select(x => new ProductDto()
                                {
                                    Id = x.Id,
                                    BarCode = x.BarCode,
                                    Description = x.Description
                                });
            }

            return Enumerable.Empty<ProductDto>();
        }

        public IEnumerable<ProductDto> ListAllProducts()
        {
            var products = GetProductsFromCache();

            return products.Select(x => new ProductDto()
            {
                Id = x.Id,
                BarCode = x.BarCode,
                Description = x.Description
            });
        }

        private List<Product> GetProductsFromCache()
        {
            if (_cache.TryGetValue(productListCacheKey, out List<Product> productList))
            {
                productList = _cache.Get<List<Product>>(productListCacheKey);

                return productList;
            }

            return Enumerable.Empty<Product>().ToList();
        }

        private void AddProductOnCache(Product product)
        {
            var products = GetProductsFromCache();

            products.Add(product);

            _cache.Remove(productListCacheKey);

            var options = new MemoryCacheEntryOptions().SetSize(products.Count());

            _cache.Set<List<Product>>(productListCacheKey, products, options);
        }
    }
}
