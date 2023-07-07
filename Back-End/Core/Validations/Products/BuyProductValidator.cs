using Dietbox.ECommerce.Core.Commands.Products;
using Dietbox.ECommerce.Core.Interfaces;
using Dietbox.ECommerce.ORM.Entities.Products;
using Dietbox.ECommerce.ORM.Interfaces;
using Dietbox.ECommerce.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Validations.Products
{
    public class BuyProductValidator : IValidator<BuyProductCommand>
    {

        private readonly IRepository<Product> _repository;
        private readonly ITenant _tenant;

        public BuyProductValidator(
            IRepository<Product> repository,
            ITenant tenant
        )
        {
            _repository = repository;
            _tenant = tenant;
        }

        public async Task<(bool isValid, List<string> messages)> Validate(BuyProductCommand command)
        {
            if (command is null)
                return (false, new() { "Parâmetro inválido." });

            if (_tenant.Type is not TenantType.Customer)
                return (false, new() { "Apenas clientes podem efetuar compras de produtos." });

            if (command.Amount is 0)
                return (false, new() { "Quantidade não pode ser igual à zero." });

            bool existsProduct = await _repository.Get(_ => _.ID == command.ID).AnyAsync();
            if (existsProduct is false)
                return (false, new() { "Produto não encontrado para efetuar a compra." });

            int availableStock = await _repository
                .Get(_ => _.ID == command.ID)
                .Select(_ => _.Stock)
                .FirstOrDefaultAsync();

            if (availableStock is 0)
                return (false, new() { "Produto sem estoque." });

            if (command.Amount > availableStock)
                return (false, new() { "A quantidade solicitada é maior que o estoque disponível do produto." });

            return (true, new());
        }
    }
}
