using System.IO;
using Sazonoff.CodeGenerator;
using UnityEditor;
using UnityEngine;

public class ExampleGenerated
{
    //[MenuItem("Class Generator/Generate Example")]
    public static void GenerateExample()
    {
        GeneratedInterface exampleInterface = new GeneratedInterface("IExampleClass", null);
        var interfaceMethod = new GeneratedInterfaceMethod("ExampleMethod");
        exampleInterface.AddMethod(interfaceMethod);

        GeneratedClass exampleClass =
            new GeneratedClass("ExampleClass", new[] { "MonoBehaviour", exampleInterface.GetName() },
                "Sazonoff.ExampleNameSpace");
        exampleClass.AddUsing("UnityEngine");
        var exampleField = new GeneratedField("ExampleField", "int", GeneratedAccessType.@public,
            defaultValue: "123");
        exampleClass.AddField(exampleField);

        var property = new GeneratedProperty("ExampleProperty", "bool", GeneratedAccessType.@public,
            GeneratedAccessType.Hidden);

        exampleClass.AddProperty(property);

        var exampleMethod = new GeneratedMethod(GeneratedAccessType.@public, "ExampleMethod");

        exampleMethod.AddBody("var a = ExampleField;");
        exampleMethod.AddBody("var b = ExampleProperty;");
        exampleClass.AddMethod(exampleMethod);

        Debug.Log(exampleInterface.ToCode());
        Debug.Log(exampleClass.ToCode());

        //SaveClassesToDisk(exampleInterface, exampleClass);
    }

    private static void SaveClassesToDisk(GeneratedInterface generatedInterface, GeneratedClass generatedClass)
    {
        var interfaceCode = generatedInterface.ToCode();
        var classCode = generatedClass.ToCode();
        var pathToInterface = Path.Combine(Application.dataPath, "IExampleClass.cs");
        var pathToClass = Path.Combine(Application.dataPath, "ExampleClass.cs");
        File.WriteAllText(pathToInterface, interfaceCode);
        File.WriteAllText(pathToClass, classCode);
        AssetDatabase.Refresh();
    }
}