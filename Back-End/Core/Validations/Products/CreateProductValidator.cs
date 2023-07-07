using Dietbox.ECommerce.Core.Commands.Products;
using Dietbox.ECommerce.Core.Interfaces;
using Dietbox.ECommerce.Tenant;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Validations.Products
{
    public class CreateProductValidator : IValidator<CreateProductCommand>
    {
        private readonly ITenant _tenate;
        private readonly CultureInfo _cultureInfo;
        private readonly TextInfo _textInfo;
        private readonly List<string> _messages;

        private const int _NAME_MAX_LENGTH = 100;
        private const int _DESCRIPTION_MAX_LENGTH = 400;
        private const int _BRAND_MAX_LENGTH = 50;
        private const int _CODE_MAX_LENGTH = 50;

        public CreateProductValidator(ITenant tenate)
        {
            _tenate = tenate;
            _cultureInfo = Thread.CurrentThread.CurrentCulture;
            _textInfo = _cultureInfo.TextInfo;
            _messages = new();
        }

        public async Task<(bool isValid, List<string> messages)> Validate(CreateProductCommand command)
        {
            if (command is null)
                return (false, new() { "Parâmetro inválido." });

            if (_tenate.Type is not TenantType.Company)
                return (false, new() { "Somente empresas podem cadastrar produtos." });

            #region Nome do Produto

            if (string.IsNullOrEmpty(command.Name))
            {
                _messages.Add("O campo 'Nome do produto' é obrigatório.");
            }
            else
            {
                command.Name = command.Name.Trim();
                command.Name = _textInfo.ToTitleCase(command.Name);

                if (command.Name.Length > _NAME_MAX_LENGTH)
                    _messages.Add(string.Format("o campo 'Nome do produto' não pode exceder o limite de {0} caracteres.", _NAME_MAX_LENGTH));

                if (command.Name.All(char.IsDigit))
                    _messages.Add("O campo 'Nome da produto' não pode conter somente números.");
            }

            #endregion


            #region Descrição do Produto

            if (!string.IsNullOrEmpty(command.Description))
            {
                command.Description = command.Description.Trim();

                if (command.Description.Length > _DESCRIPTION_MAX_LENGTH)
                    _messages.Add(string.Format("o campo 'Descrição do produto' não pode exceder o limite de {0} caracteres.", _DESCRIPTION_MAX_LENGTH));

            }

            #endregion

            #region Marca do Produto

            if (!string.IsNullOrEmpty(command.Brand))
            {
                command.Brand = command.Brand.Trim();

                if (command.Brand.Length > _BRAND_MAX_LENGTH)
                    _messages.Add(string.Format("o campo 'Marca do produto' não pode exceder o limite de {0} caracteres.", _BRAND_MAX_LENGTH));

            }

            #endregion


            #region Preço do Produto

            if (command.Price is < 0)
                _messages.Add("O valor do produto não pode ser inferior à 0 (zero).");

            if (command.Price is 0)
                _messages.Add("O valor do produto não pode ser igual à 0 (zero).");

            #endregion


            #region Código Interno do Produto

            if (!string.IsNullOrEmpty(command.Code))
            {
                command.Code = command.Code.Trim();

                if (command.Code.Length > _CODE_MAX_LENGTH)
                    _messages.Add(string.Format("o campo 'Código do produto' não pode exceder o limite de {0} caracteres.", _CODE_MAX_LENGTH));

            }

            #endregion

            #region Estoque do Produto

            if (command.Stock is < 0)
                _messages.Add("O estoque do produto não pode ser inferior à 0 (zero).");

            //if (command.Stock is 0)
            //    _messages.Add("O estoque do produto não pode ser igual à 0 (zero).");

            #endregion


            return (!_messages.Any(), _messages);

        }
    }
}
