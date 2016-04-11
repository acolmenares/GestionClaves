using GestionClaves.Modelos.Interfaces;
using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Results;

namespace GestionClaves.BL.Validadores
{
    public abstract class ValidadorBase<T> : AbstractValidator<T>, IValidador<T>
    {
        public string MensajeCuandoInstanciaEsNull { get; set; }
        public ValidadorBase()
        {
            MensajeCuandoInstanciaEsNull = string.Format("{0}={1}", typeof(T).Name, "null");
        }
        public override ValidationResult Validate(T instance)
        {
            return instance == null
                ? new ValidationResult(new[] { new ValidationFailure(typeof(T).Name, MensajeCuandoInstanciaEsNull, "") })
                : base.Validate(instance);
        }

        public void ValidateAndThrow(T instance)
        {
            DefaultValidatorExtensions.ValidateAndThrow<T>(this, instance);
        }
    }
}
