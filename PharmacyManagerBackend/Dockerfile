FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Exceptions/Exceptions.csproj", "Exceptions/"]
COPY ["IBusinessLogic/IBusinessLogic.csproj", "IBusinessLogic/"]
COPY ["ExporterDomain/ExporterDomain.csproj", "ExporterDomain/"]
COPY ["Factory/Factory.csproj", "Factory/"]
COPY ["AuthLogic/AuthLogic.csproj", "AuthLogic/"]
COPY ["IAuthLogic/IAuthLogic.csproj", "IAuthLogic/"]
COPY ["IDataAccess/IDataAccess.csproj", "IDataAccess/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
COPY ["ExporterInterface/ExporterInterface.csproj", "ExporterInterface/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["WebApi.Filter/WebApi.Filter.csproj", "WebApi.Filter/"]
COPY ["WebApi.Models/WebApi.Models.csproj", "WebApi.Models/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
