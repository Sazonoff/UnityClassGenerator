using System.Collections.Generic;

namespace Sazonoff.CodeGenerator
{
    public class GeneratedInterfaceMethod : IGeneratorResult
    {
        private string returnType;
        private string methodName;
        private List<GeneratedMethodParameter> parameters = new();

        public GeneratedInterfaceMethod(string methodName, string returnType = "void")
        {
            this.returnType = returnType;
            this.methodName = methodName;
        }

        public string ToCode()
        {
            string code = $"\t\tpublic {returnType} {methodName}(";
            for (int i = 0; i < parameters.Count; i++)
            {
                code += parameters[i].ToCode();
                if (i < parameters.Count - 1)
                {
                    code += ",";
                }
            }

            code += ");";
            return code;
        }
    }
}