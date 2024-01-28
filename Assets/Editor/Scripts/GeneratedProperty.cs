using System;

namespace Sazonoff.CodeGenerator
{
    public class GeneratedProperty : IGeneratorResult
    {
        public GeneratedAccessType GetAccessType { get; private set; }
        public GeneratedAccessType SetAccessType { get; private set; }
        private string PropertyType;
        private string PropertyName;
        private string DefaultValue;

        public GeneratedProperty(string propertyName, string propertyType, GeneratedAccessType getAccessType,
            GeneratedAccessType setAccessType, string defaultValue = null)
        {
            this.GetAccessType = getAccessType;
            this.SetAccessType = setAccessType;
            this.PropertyType = propertyType;
            this.PropertyName = propertyName;
            this.DefaultValue = defaultValue;
        }

        public string ToCode()
        {
            string code = $"\t\t{GetAccessType.ToString()} {PropertyType} {PropertyName} ";
            code += GetSetAccessor();
            if (DefaultValue != null && DefaultValue.Length > 0)
            {
                code += $" = {DefaultValue};";
            }

            return code;
        }

        private string GetSetAccessor()
        {
            string accesses = String.Empty;

            if (GetAccessType == GeneratedAccessType.@public)
            {
                accesses = $"{{ get; ";
                if (SetAccessType == GeneratedAccessType.Hidden)
                {
                    accesses += $"}}";
                    return accesses;
                }

                accesses += $" set; }}";
                return accesses;
            }

            if (GetAccessType == GeneratedAccessType.@protected)
            {
                accesses = $"{{{{ {GetAccessType.ToString()} get; ";

                if (SetAccessType == GeneratedAccessType.Hidden)
                {
                    accesses += $"}}";
                    return accesses;
                }

                accesses += $" set; }}";
                return accesses;
            }

            if (GetAccessType == GeneratedAccessType.@private)
            {
                accesses = $"{{ get; ";

                if (SetAccessType == GeneratedAccessType.Hidden)
                {
                    accesses += $"}}";
                    return accesses;
                }

                return $"{{ get; set; }}";
            }

            return $"{{ get; set; }}";
        }
    }
}