# Karcags.Blazor.Common

> Common Blazor Library

## HTTP

### Install

1. Register `HelperService`
2. Register `HttpService`
   - Add Service as Scoped service into `Startup.cs` or `Program.cs`
   ```c#
   services.AddScoped<IHttpService, HttpService>();
   ```
   or
   ```c#
   builder.Services.AddScoped<IHttpService, HttpService>();
   ```
3. Use it.

## Helper

### Install

1. Register `HelperService`
    - Add Service as Scoped service into `Startup.cs` or `Program.cs`
   ```c#
   services.AddScoped<IHelperService, HelperService>();
   ```
   or
   ```c#
   builder.Services.AddScoped<IHelperService, HelperService>();
   ```
   
## Common Service

> Service for common HTTP calls
> - Get element by Id
> - Get element lists
> - Post new element
> - Put element
> - Remove element

## Modal

1. Register `ModalService`
   - Add Service as Scoped service into `Startup.cs` or `Program.cs`
   ```c#
   services.AddScoped<IModalService, ModalService>();
   ```
   or
   ```c#
   builder.Services.AddScoped<IModalService, ModalService>();
   ```
2. Add BlazoredModal to `App.razor` file.
```
<BlazoredModal></BlazoredModal>
```
   
## Components

- Add using into `_Imports.razor`
```
@using Karcags.Blazor.Common
```

### Loader Component

```
<Loader></Loader>
```

### SimpleList

### ListTable