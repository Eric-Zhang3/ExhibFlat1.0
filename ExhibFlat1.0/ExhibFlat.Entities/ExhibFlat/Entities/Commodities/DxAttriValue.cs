namespace ExhibFlat.Entities.Commodities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class DxAttriValue
    {
        public int? DefValueId{ get; set; }

        public int? AttriDefID{ get; set; }

        public string ValueStrDef{ get; set; }
    }
}
