using System;
using System.Collections.Generic;
using System.Linq;

namespace Sazonoff.CodeGenerator
{
    public class GeneratedClass : IGeneratorResult
    {
        private string className;
        private string nameSpace;
        private List<string> usings = new();
        private List<GeneratedField> fields = new();
        private List<GeneratedProperty> properties = new();
        private List<GeneratedMethod> methods = new();
        private bool hasBaseClass;
        private bool isStatic;
        private string[] baseClasses;

        public GeneratedClass(string className, string[] baseClasses, string nameSpace = null, bool isStatic = false)
        {
            this.className = className;
            this.nameSpace = nameSpace;
            this.hasBaseClass = baseClasses != null && baseClasses.Length > 0;
            this.baseClasses = baseClasses;
            this.isStatic = isStatic;
        }

        public void AddProperty(GeneratedProperty property)
        {
            properties.Add(property);
            properties = properties.OrderByDescending(p => p.GetAccessType).ToList();
        }

        public void AddField(GeneratedField field)
        {
            fields.Add(field);
            fields = fields.OrderByDescending(f => f.AccessType).ToList();
        }

        /// <summary>
        /// Without "", using and ;
        /// Just
        /// System
        /// </summary>
        /// <param name="usingName"></param>
        public void AddUsing(string usingName)
        {
            usings.Add(usingName);
        }

        public void AddMethod(GeneratedMethod method)
        {
            methods.Add(method);
            methods = methods.OrderByDescending(p => p.AccessType).ToList();
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

            code += GetClassTittle();
            code += "\n";
            code += "\t{";
            foreach (var field in fields)
            {
                code += "\n";
                code += field.ToCode();
            }

            foreach (var property in properties)
            {
                code += "\n";
                code += property.ToCode();
            }

            if ((fields.Count > 0 || properties.Count > 0) && methods.Count > 0)
            {
                code += "\n";
            }

            for (int i = 0; i < methods.Count; i++)
            {
                if (i > 0)
                {
                    code += "\n";
                }

                code += "\n";
                code += methods[i].ToCode();
            }

            code += "\n\t}\n";
            if (nameSpace != null && nameSpace.Length > 0)
            {
                code += "}";
            }

            return code;
        }

        private string GetClassTittle()
        {
            var code = $"\tpublic ";
            if (isStatic)
            {
                code += "static ";
            }

            code += $"class {className}";
            if (hasBaseClass)
            {
                code += $" : ";
                for (int i = 0; i < baseClasses.Length; i++)
                {
                    if (i > 0)
                    {
                        code += ", ";
                    }

                    code += $"{baseClasses[i]}";
                }
            }

            return code;
        }
    }
}