namespace Sazonoff.CodeGenerator
{
    public class GeneratedMethodParameter : IGeneratorResult
    {
        private string parameterName;
        private string parameterType;
        private string defaultValue;
        private bool isThis;

        public GeneratedMethodParameter(string name, string type, string defaultValue = null, bool isThis = false)
        {
            this.parameterName = name;
            this.parameterType = type;
            this.defaultValue = defaultValue;
            this.isThis = isThis;
        }

        public string ToCode()
        {
            var code = string.Empty;
            if (isThis)
            {
                code += "this ";
            }

            code += $"{parameterType} {parameterName}";
            if (defaultValue != null && defaultValue.Length > 0)
            {
                code += $" = {defaultValue}";
            }

            return code;
        }
    }
}