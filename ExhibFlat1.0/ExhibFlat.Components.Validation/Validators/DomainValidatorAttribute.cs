namespace ExhibFlat.Components.Validation.Validators
{
    using ExhibFlat.Components.Validation;
    using System;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple=true, Inherited=false)]
    public sealed class DomainValidatorAttribute : ValueValidatorAttribute
    {
        private object[] domain;

        public DomainValidatorAttribute() : this(new object[0])
        {
        }

        public DomainValidatorAttribute(params object[] domain)
        {
            ValidatorArgumentsValidatorHelper.ValidateDomainValidator(domain);
            this.domain = domain;
        }

        protected override Validator DoCreateValidator(Type targetType)
        {
            return new DomainValidator<object>(base.Negated, this.domain);
        }
    }
}

