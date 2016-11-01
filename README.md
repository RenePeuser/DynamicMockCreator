# DynamicMockCreator

This project was made to simplify unit test. The problem which we have in our daily work is, that you have to create big data structures to test some components. If you only interesting in, that this data container goes from component A to component B. Your are not interesting of the specific data of that container.

For this reason i create an "ObjectCreator" which can create data structures dynamically.

# Simple sample

This Samples shows the default usage of the object creator. That means the data structures will be initialized with default datas, which comes from the object creator.

Create from Interface:


```csharp
[TestMethod]
public void CreateFromInterface()
{
    Assert.IsNotNull(ObjectCreatorExtensions.Create<INotifyPropertyChanged>());
}
```

Create from class

```csharp
[TestMethod]
public void CreateContainerClass()
{
    Assert.IsNotNull(ObjectCreatorExtensions.Create<Container>());
}
```

Create from abstract class

```csharp
[TestMethod]
public void CreateFromAbstractClass()
{
    Assert.IsNotNull(ObjectCreatorExtensions.Create<ClassAbstract>());
}
```

# Radom data initialization

Another possibility is to generate the data by random process.
The first thing what we need is an random data creator.

private static readonly RandomDefaultData RandomDefaultData = new RandomDefaultData();

If we has this creator we can use it as an parameter for the "Create<>" method. With this way
the data structures will be initialized with randomized data.


```csharp
[TestMethod]
public void CreateFromInterfaceType()
{
    Assert.IsNotNull(ObjectCreatorExtensions.Create<INotifyPropertyChanged>(RandomDefaultData));
}
```


```csharp
[TestMethod]
public void CreateFromClassWithPrimitiveArguments()
{
    Assert.IsNotNull(ObjectCreatorExtensions.Create<ClassWithPrimitiveTypes>(RandomDefaultData));
}
```


```csharp
[TestMethod]
public void CreateFromClassWithInterfacesArguments()
{
    Assert.IsNotNull(ObjectCreatorExtensions.Create<ClassWithInterfaces>(RandomDefaultData));
}
```

