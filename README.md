# dotnet_common
Commonly used helpers for dotnet core.

## Installation
This package can be installed via nuget using:

```
Install-Package dotnet_common -Version 1.0.0
```

Or via the dotnet CLI using:

```
dotnet add package dotnet_common --version 1.0.0
```

## Usage

For use with the built in dependency injection framework of a .net core web app / api, install the package (as per above) and then add whichever service you would like to use to your ConfigureServices method in your startup.cs class:

```
//cache manager class added as a singleton to keep one instance alive in your application
services.AddSingleton<dotnet_common.Interface.ICacheManager, dotnet_common.CacheManager.MemoryCache>();

//file system utility
services.AddScoped<dotnet_common.Interface.IFileSystemUtility, dotnet_common.FileSystemUtility.SystemIO>();

//marshaller that uses the data contract serializer
services.AddScoped<dotnet_common.Interface.IMarshaller, dotnet_common.Marshaller.DataContractSerializer>();
```

Next you can use whichever class you want in your controllers by adding them like so:

```
[Route("api/[controller]")]
public class ValuesController : Controller
{
    private readonly dotnet_common.Interface.ICacheManager _cacheManager;
    private readonly dotnet_common.Interface.IMarshaller _marshaller;
    private readonly dotnet_common.Interface.IFileSystemUtility _fileSystemUtility;

    public [ControllerName]Controller(dotnet_common.Interface.ICacheManager cacheManager,
        dotnet_common.Interface.IMarshaller marshaller,
        dotnet_common.Interface.IFileSystemUtility fileSystemUtility)
    {
        _cacheManager = cacheManager;
        _marshaller = marshaller;
        _fileSystemUtility = fileSystemUtility;
    }
}
```

You would then be able to use the privately instantiated version of the relevant class in your code.

## Samples

There is a sample project included in the code to show usage in a dotnet core web API project.

* [Sample Project](https://github.com/heathesh/dotnet_common/tree/master/sample/dotnet_common.Sample.Web)
* [Sample Startup Class](https://github.com/heathesh/dotnet_common/blob/master/sample/dotnet_common.Sample.Web/Startup.cs)

* [Cache Manager being used in a controller](https://github.com/heathesh/dotnet_common/blob/master/sample/dotnet_common.Sample.Web/Controllers/CacheSampleController.cs)
* [File System Utility being used in a controller](https://github.com/heathesh/dotnet_common/blob/master/sample/dotnet_common.Sample.Web/Controllers/FileSystemUtilitySampleController.cs)
* [Marshaller being used in a controller](https://github.com/heathesh/dotnet_common/blob/master/sample/dotnet_common.Sample.Web/Controllers/MarshallerSampleController.cs)