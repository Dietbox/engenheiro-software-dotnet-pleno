using ECommerce.Domain.Dto;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task CreateProduct(ProductInputModel product, string userEmail);
        IEnumerable<ProductDto> ListAllProducts();
        Task<IEnumerable<ProductDto>> ListProductsByCompany(string userEmail);
    }
}
