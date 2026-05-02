# Setup

This repository is a custom `dotnet new` template for generating a Clean Architecture microservice.

It supports:

- Solution name from `-n`
- Service name from `--ServiceName`
- Folder rename such as `src/__ServiceName__` -> `src/Ecommerce`
- Project names, file names, namespaces, and references based on `ServiceName`

## Prerequisites

- .NET SDK installed
- PowerShell or terminal access

Check SDK:

```powershell
dotnet --info
```

## Template Authoring Rules

Use these conventions inside the template source:

- Keep solution placeholder as `TemplateSolution`
- Keep service placeholder as `__ServiceName__`
- Use `__ServiceName__` in:
  - folder names
  - project file names
  - namespaces
  - `ProjectReference` paths
  - Dockerfile paths

Do not use:

```text
src/{ServiceName}
tests/{ServiceName}.Tests
```

Use:

```text
src/__ServiceName__
tests/__ServiceName__.Tests
```

## Expected Template Structure

```text
CleanArchitectureTemplate/
  .template.config/
    template.json
  TemplateSolution.sln
  src/
    __ServiceName__/
      __ServiceName__.API/
      __ServiceName__.Application/
      __ServiceName__.Domain/
      __ServiceName__.Infrastructure/
  tests/
    __ServiceName__.Tests/
      __ServiceName__.Application.UnitTests/
      __ServiceName__.Architecture.Tests/
      __ServiceName__.Domain.UnitTests/
      __ServiceName__.Integration.Tests/
```

## Build The Template Package

From the repository root:

```powershell
dotnet pack .\TemplateProject.csproj -c Release
```

Package output:

```text
.\nupkg\
```

## Install The Template Locally

Install directly from the repository:

```powershell
dotnet new install .
```

Reinstall after changes:

```powershell
dotnet new install . --force
```

Clear template cache if needed:

```powershell
dotnet new --debug:reinit
```

## Create A New Microservice

Example:

```powershell
dotnet new cleanarch -n MySolution --ServiceName Ecommerce
```

This should generate:

```text
MySolution.sln
src/Ecommerce/
tests/Ecommerce.Tests/
```

## Create In A Specific Output Folder

Recommended during testing so the generated project does not end up inside the template repository:

```powershell
dotnet new cleanarch -n MySolution --ServiceName Ecommerce -o C:\tmp\MySolution
```

This is important because generating inside the template repo can accidentally create nested output that later gets packed or copied if exclusions are wrong.

## Safe Local Testing Workflow

Use a custom hive so you do not depend on global template cache state:

```powershell
dotnet new install . --force --debug:custom-hive .template-verify-hive
dotnet new cleanarch -n MySolution --ServiceName Ecommerce -o C:\tmp\MySolution --debug:custom-hive .template-verify-hive
```

## Verify Folder Rename Works

After generation, verify these paths exist:

```text
src/Ecommerce/Ecommerce.API/Ecommerce.API.csproj
src/Ecommerce/Ecommerce.Application/Ecommerce.Application.csproj
src/Ecommerce/Ecommerce.Domain/Ecommerce.Domain.csproj
src/Ecommerce/Ecommerce.Infrastructure/Ecommerce.Infrastructure.csproj
tests/Ecommerce.Tests/Ecommerce.Application.UnitTests/Ecommerce.Application.UnitTests.csproj
```

## How Rename Works

- `sourceName: "TemplateSolution"` controls the solution name from `-n`
- `replaces: "__ServiceName__"` replaces text inside files
- `fileRename: "__ServiceName__"` renames files and folders containing `__ServiceName__`

If folder names appear literally as `{ServiceName}`, it means the template is using the wrong rename mechanism.

## Common Commands

List installed templates:

```powershell
dotnet new list cleanarch
```

Uninstall template:

```powershell
dotnet new uninstall CleanArchitecture.Template
```

If uninstall by identity does not work, uninstall by source path:

```powershell
dotnet new uninstall D:\Muskan\CleanArchitectureTemplate
```

## Common Pitfalls

- Using `{ServiceName}` in physical folder names
- Forgetting `fileRename` in `template.json`
- Testing from stale template cache
- Generating output inside the template repository root
- Packing local test output folders into the `.nupkg`
- Leaving authoring-only files in the template, such as:
  - local hives
  - generated sample output
  - helper scripts not meant for consumers
  - `.template.config` solution items in the generated `.sln`

## Recommended Development Loop

1. Update template files.
2. Reinstall with `dotnet new install . --force`.
3. Generate to a clean external folder with `-o`.
4. Verify:
   - solution name
   - folder rename
   - project references
   - namespaces
   - Dockerfile paths
5. Pack only after verification succeeds.

## Notes

- Keep repo-local test folders out of source control.
- Keep repo-local test folders out of the packed template.
- Prefer testing with a custom hive when debugging rename issues.
```sh
dotnet new uninstall D:\Muskan\CleanArchitectureTemplate
dotnet new --debug:reinit
dotnet new install D:\Muskan\CleanArchitectureTemplate
```

```sh
mkdir D:\Muskan\TestOut
cd D:\Muskan\TestOut
dotnet new cleanarch -n MySolution --ServiceName Ecommerce -o D:\Muskan\TestOut\MySolution
```