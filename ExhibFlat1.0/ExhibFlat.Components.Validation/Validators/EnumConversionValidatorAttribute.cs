namespace ExhibFlat.Components.Validation.Validators
{
    using ExhibFlat.Components.Validation;
    using System;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple=true, Inherited=false)]
    public sealed class EnumConversionValidatorAttribute : ValueValidatorAttribute
    {
        private Type enumType;

        public EnumConversionValidatorAttribute(Type enumType)
        {
            ValidatorArgumentsValidatorHelper.ValidateEnumConversionValidator(enumType);
            this.enumType = enumType;
        }

        protected override Validator DoCreateValidator(Type targetType)
        {
            return new EnumConversionValidator(this.enumType, base.Negated);
        }
    }
}

