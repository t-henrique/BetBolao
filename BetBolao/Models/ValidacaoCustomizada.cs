using System;
using System.ComponentModel.DataAnnotations;

namespace BetBolao.Compartilhado.Models
{
    public class ValidacaoCustomizada: ValidationAttribute
    {
        private readonly DateTime _dataAtual;

        public ValidacaoCustomizada(DateTime dataAtual)
        {
            _dataAtual = dataAtual;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (_dataAtual.Date >= DateTime.Now.Date)
                {
                    var erroMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(erroMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
