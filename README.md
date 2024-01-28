# What is it?
Simple class generator for unity editor. It uses object-like building of classes/interfaces.
```
var exampleClass = new GeneratedClass("ExampleClass", new[] { "MonoBehaviour", exampleInterface.GetName() }, "Sazonoff.ExampleNameSpace");
exampleClass.AddUsing("UnityEngine");

var exampleField = new GeneratedField("ExampleField", "int", GeneratedAccessType.@public, defaultValue: "123");
exampleClass.AddField(exampleField);

Debug.Log(exampleClass.ToCode());

```

# How to install?

## Installing base
Check unity documentation on installing git packages: https://docs.unity3d.com/2019.3/Documentation/Manual/upm-git.html   

### Using package manager:
```
Window -> Package Manager -> '+' -> Add package from git url https://github.com/Sazonoff/UnityClassGenerator.git?path=Assets
```

### Using manifest.json
```
{
  "dependencies": {
    "com.sazonoff.unity-class-generator": "https://github.com/Sazonoff/UnityClassGenerator.git?path=Assets"
}
```

# How to use

In your editor scripts(EditorWindow/CustomEditor e.t.c.) use this classes:
- GeneratedInterface
- GeneratedClass
- GeneratedField
- GeneratedMethod
- GeneratedMethodParameter
- GeneratedProperty

After use .ToCode() to get generated class. Save it. Enjoy.
Check sample folder for more detailed example.