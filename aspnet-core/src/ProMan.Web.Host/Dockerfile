FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/ProMan.Web.Host/ProMan.Web.Host.csproj", "src/ProMan.Web.Host/"]
COPY ["src/ProMan.Web.Core/ProMan.Web.Core.csproj", "src/ProMan.Web.Core/"]
COPY ["src/ProMan.Application/ProMan.Application.csproj", "src/ProMan.Application/"]
COPY ["src/ProMan.Core/ProMan.Core.csproj", "src/ProMan.Core/"]
COPY ["src/ProMan.EntityFrameworkCore/ProMan.EntityFrameworkCore.csproj", "src/ProMan.EntityFrameworkCore/"]
WORKDIR "/src/src/ProMan.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/ProMan.Web.Host", "src/ProMan.Web.Host"]
COPY ["src/ProMan.Web.Core", "src/ProMan.Web.Core"]
COPY ["src/ProMan.Application", "src/ProMan.Application"]
COPY ["src/ProMan.Core", "src/ProMan.Core"]
COPY ["src/ProMan.EntityFrameworkCore", "src/ProMan.EntityFrameworkCore"]
WORKDIR "/src/src/ProMan.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "ProMan.Web.Host.dll"]
