namespace Sazonoff.CodeGenerator
{
    public class GeneratedField : IGeneratorResult
    {
        public GeneratedAccessType AccessType { get; private set; }
        private string fieldName;
        private string fieldType;
        private bool isConst = false;
        private bool isStatic = false;
        private bool isReadOnly = false;
        private string defaultValue;

        public GeneratedField(string fieldName, string fieldType, GeneratedAccessType accessType, bool isConst = false,
            bool isStatic = false, bool isReadOnly = false, string defaultValue = "")
        {
            this.fieldName = fieldName;
            this.fieldType = fieldType;
            this.AccessType = accessType;
            this.isConst = isConst;
            this.isStatic = isStatic;
            this.isReadOnly = isReadOnly;
            this.defaultValue = defaultValue;
        }

        public string ToCode()
        {
            var code = $"\t\t{AccessType.ToString()} ";
            if (isConst)
            {
                code += "const ";
            }
            else if (isStatic)
            {
                code += "static ";
            }
            else if(isReadOnly)
            {
                code += "readonly ";
            }

            code += $"{fieldType} {fieldName}";
            if (defaultValue.Length > 0)
            {
                code += $" = {defaultValue}";
            }

            code += ";";
            return code;
        }
    }
}