# DotNetRastreioSearh  [![Nuget](https://img.shields.io/nuget/v/DotNetRastreioSearch)](https://www.nuget.org/packages/DotNetRastreioSearch/) ![Nuget](https://img.shields.io/nuget/dt/DotNetRastreioSearch)
It is a library that helps you to track your objects in Correios

## Notes
Version 1.0.1:

- Upgrade to .NET 6

## Installation

Use the package manager to install.

```bash
Install-Package DotNetRastreioSearch -Version 1.0.1
```

## Usage

After install:
```C#
using DotNet.Rastreio.Search.App;
```
Get Objects by tracking code
```C#
RastreioSearch rastreio = new RastreioSearch();
string jsonResultAsync = await GetObjetoRastreioAsync("trackingCode");

string jsonResult = GetObjetoRastreio("trackingCode");
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
