```sh
dotnet pack TemplateProject.csproj -c Release -o ./nupkg
```
```sh
dotnet new uninstall Coreplex.CleanArchitecture.Template.Microservices
```
```sh
 dotnet new install ./nupkg/Coreplex.CleanArchitecture.Template.Microservices.2.0.4.nupkg
 ```