# Getting started with ixassignment-api

## Environment:
- `dotnet sdk 6.0` - https://dotnet.microsoft.com/en-us/download/dotnet/6.0 (install at minimum 6.0.16)
- postgresql 
- Visual Studio Community 2019 

## Configurations:
- Update Postgresql connection string in Intelexual.API > appsettings.Development.json file. 
- Packages will be restored automatically when opening the solution in Visual Studio. Will also work in Visual Studio Code with dotnet cli commands. 

## Run project 
- Ensure Intelexual.API is set as startup project. (Not needed if using cli)
- Click the Run button in Visual Studio (or) execute the command `run dotnet` in the Intelexual.API directory. 
- Note the port number that is being used (6005) and check it with the react app API_BASE_URl variable.

