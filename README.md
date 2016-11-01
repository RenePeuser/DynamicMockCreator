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

# DesignTimeCreator

Another cool thing what we have with this creator now, is that we can use it to create our DesignTime-DataContext for XAML files. 
Every developer knows how much time it costs to have the XAML Designer with valid informations. Everytime you have to create mock ups, which will get no service over the time. Every time if the interfaces are changed, you have a lot of refactorings to do. Every of us know that we produce code in our view assemblies which are not necssary for the prodcution code, but you have to implement it to see some visual result at design time.

The solution for such problems is, that you can combine this object creator with the design time data context.

To solve this quite easy, i have written a very small attached property which sets the DataContext ONLY at design time with the expected type.

DesignTimeAttachedProperties.DesignTimeType="{ExpectedType}" in the brackets you has to put in your type which has to be the design time DataContext.

Sample with a concrete view model "MainWindowViewModel"

```xaml
<Window x:Class="DesignTimeCreator.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:DesignTimeCreator"
        mc:Ignorable="d"
        Title="MainWindow" Height="549.2" Width="520.2"
        local:DesignTimeAttachedProperties.DesignTimeType="{x:Type local:MainWindowViewModel}">
    <StackPanel>
        <TextBlock Text="{Binding Path=IntValue}"/>
        <TextBlock Text="{Binding Path=DoubleValue}"/>
        <TextBlock Text="{Binding Path=DecimalValue}"/>
        <TextBlock Text="{Binding Path=BoolValue}"/>
        <TextBlock Text="{Binding Path=Person.Name}"/>
        <TextBlock Text="{Binding Path=Person.Age}"/>
        <DataGrid ItemsSource="{Binding Path=Persons}" AutoGenerateColumns="True"/>
        <DataGrid ItemsSource="{Binding Path=PersonArray}" AutoGenerateColumns="True"/>
        <DataGrid ItemsSource="{Binding Path=PersonOC}" AutoGenerateColumns="True"/>
    </StackPanel>
</Window>
```

Sample with an interface view model "MainWindowViewModel"

```xaml
<Window x:Class="DesignTimeCreator.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:DesignTimeCreator"
        mc:Ignorable="d"
        Title="MainWindow" Height="549.2" Width="520.2"
        local:DesignTimeAttachedProperties.DesignTimeType="{x:Type local:IMainWindowViewModel}">
    <StackPanel>
        <TextBlock Text="{Binding Path=IntValue}"/>
        <TextBlock Text="{Binding Path=DoubleValue}"/>
        <TextBlock Text="{Binding Path=DecimalValue}"/>
        <TextBlock Text="{Binding Path=BoolValue}"/>
        <TextBlock Text="{Binding Path=Person.Name}"/>
        <TextBlock Text="{Binding Path=Person.Age}"/>
        <DataGrid ItemsSource="{Binding Path=Persons}" AutoGenerateColumns="True"/>
        <DataGrid ItemsSource="{Binding Path=PersonArray}" AutoGenerateColumns="True"/>
        <DataGrid ItemsSource="{Binding Path=PersonOC}" AutoGenerateColumns="True"/>
    </StackPanel>
</Window>
```
