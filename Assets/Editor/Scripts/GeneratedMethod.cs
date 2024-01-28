using System;
using System.Collections.Generic;

namespace Sazonoff.CodeGenerator
{
    public class GeneratedMethod : IGeneratorResult
    {
        public GeneratedAccessType AccessType { get; private set; }
        private string returnType;
        private string methodName;
        private List<GeneratedMethodParameter> parameters = new();
        private List<string> bodyParts = new();
        private List<Attribute> attributes = new();
        private bool isOverride;
        private bool isStatic;
        private const int AttributeWordSymbols = 9;

        public GeneratedMethod(GeneratedAccessType accessType, string methodName, string returnType = "void",
            bool isOverride = false, bool isStatic = false)
        {
            this.AccessType = accessType;
            this.returnType = returnType;
            this.methodName = methodName;
            this.isOverride = isOverride;
            this.isStatic = isStatic;
        }

        public void AddParameter(GeneratedMethodParameter param)
        {
            parameters.Add(param);
        }

        public void AddBody(string bodyPart)
        {
            bodyParts.Add(bodyPart);
        }

        public void AddAttribute(Attribute attribute)
        {
            attributes.Add(attribute);
        }

        public string ToCode()
        {
            string code = String.Empty;
            if (attributes.Count > 0)
            {
                for (int i = 0; i < attributes.Count; i++)
                {
                    var attName = attributes[i].GetType().Name;
                    attName = attName.Remove(attName.Length - AttributeWordSymbols, AttributeWordSymbols);
                    code += $"\t\t[{attName}]";
                    code += "\n";
                }
            }

            code += $"\t\t{AccessType.ToString()} ";
            if (isOverride)
            {
                code += "override ";
            }
            else
            {
                if (isStatic)
                {
                    code += "static ";
                }
            }

            if (returnType.Length > 0)
            {
                code += $"{returnType} ";
            }

            code += $"{methodName}(";
            for (int i = 0; i < parameters.Count; i++)
            {
                if (i > 0)
                {
                    code += " ";
                }

                code += parameters[i].ToCode();
                if (i < parameters.Count - 1)
                {
                    code += ",";
                }
            }

            code += ")\n";
            code += "\t\t{";
            foreach (var body in bodyParts)
            {
                code += "\n";
                code += $"\t\t\t{body}";
            }

            code += "\n\t\t}";
            return code;
        }
    }
}