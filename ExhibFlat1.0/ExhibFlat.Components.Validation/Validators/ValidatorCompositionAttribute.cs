namespace ExhibFlat.Components.Validation.Validators
{
    using ExhibFlat.Components.Validation;
    using System;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class, AllowMultiple=true, Inherited=false)]
    public sealed class ValidatorCompositionAttribute : BaseValidationAttribute
    {
        private ExhibFlat.Components.Validation.CompositionType compositionType;

        public ValidatorCompositionAttribute(ExhibFlat.Components.Validation.CompositionType compositionType)
        {
            this.compositionType = compositionType;
        }

        internal ExhibFlat.Components.Validation.CompositionType CompositionType
        {
            get
            {
                return this.compositionType;
            }
        }
    }
}

