using System;
using System.Collections.Generic;
using System.Linq;

namespace Sazonoff.CodeGenerator
{
    public class GeneratedInterface : IGeneratorResult
    {
        private string interfaceName;
        private string nameSpace;
        private bool hasBaseClass;
        private string[] baseClasses;

        private List<string> usings = new();
        private List<GeneratedProperty> properties = new();
        private List<GeneratedInterfaceMethod> methods = new();

        public GeneratedInterface(string interfaceName, string[] baseClasses, string nameSpace = null)
        {
            this.interfaceName = interfaceName;
            this.nameSpace = nameSpace;
            this.hasBaseClass = baseClasses != null && baseClasses.Length > 0;
            this.baseClasses = baseClasses;
        }

        public void AddProperty(GeneratedProperty property)
        {
            properties.Add(property);
            properties = properties.OrderByDescending(p => p.GetAccessType).ToList();
        }

        public void AddUsing(string usingName)
        {
            usings.Add(usingName);
        }

        public void AddMethod(GeneratedInterfaceMethod method)
        {
            methods.Add(method);
        }

        public string ToCode()
        {
            string code = String.Empty;
            foreach (var useClass in usings)
            {
                code += $"using {useClass};\n";
            }

            if (usings.Count > 0)
            {
                code += "\n";
            }

            if (nameSpace != null && nameSpace.Length > 0)
            {
                code += $"namespace {nameSpace}\n";
                code += "{\n";
            }

            code += GetInterfaceTittle();
            code += "\n";
            code += "\t{\n";
            foreach (var property in properties)
            {
                code += property.ToCode();
                code += "\n";
            }

            foreach (var method in methods)
            {
                code += method.ToCode();
                code += "\n";
            }

            code += "\t}\n";
            if (nameSpace != null && nameSpace.Length > 0)
            {
                code += "}";
            }

            return code;
        }

        private string GetInterfaceTittle()
        {
            var code = $"\tpublic interface {interfaceName}";
            if (hasBaseClass)
            {
                code += $" : ";
                for (int i = 0; i < baseClasses.Length; i++)
                {
                    code += $"{baseClasses[i]}";
                    if (i > 0 && i < baseClasses.Length - 1)
                    {
                        code += ", ";
                    }
                }
            }

            return code;
        }

        public string GetName()
        {
            return interfaceName;
        }
    }
}