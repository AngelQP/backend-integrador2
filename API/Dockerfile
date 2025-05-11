#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API/Ferreteria.GestionVentas.API.csproj", "API/"]
COPY ["Modules/GestionVentas/Application/Ferreteria.Modules.GestionVentas.Application.csproj", "Modules/GestionVentas/Application/"]
COPY ["BuildingBlocks/Extensions/Bigstick.BuildingBlocks.csproj", "BuildingBlocks/Extensions/"]
COPY ["BuildingBlocks/Application/Bigstick.BuildingBlocks.Application.csproj", "BuildingBlocks/Application/"]
COPY ["BuildingBlocks/Domain/Bigstick.BuildingBlocks.Domain.csproj", "BuildingBlocks/Domain/"]
COPY ["Comunications/Application/Ferreteria.Comunications.Application.csproj", "Comunications/Application/"]
COPY ["Modules/GestionVentas/Domain/Ferreteria.Modules.GestionVentas.Domain.csproj", "Modules/GestionVentas/Domain/"]
COPY ["BuildingBlocks/ObjectStick/Bigstick.BuildingBlocks.ObjectStick.csproj", "BuildingBlocks/ObjectStick/"]
COPY ["BuildingBlocks/EventBus/Bigstick.BuildingBlocks.EventBus.csproj", "BuildingBlocks/EventBus/"]
COPY ["BuildingBlocks/Reports/Bigstick.BuildingBlocks.Reports.csproj", "BuildingBlocks/Reports/"]
COPY ["Modules/GestionVentas/Infrastructure/Ferreteria.Modules.GestionVentas.Infrastructure.csproj", "Modules/GestionVentas/Infrastructure/"]
COPY ["Comunications/Infraestructure/Ferreteria.Comunications.Infrastructure.csproj", "Comunications/Infraestructure/"]
COPY ["BuildingBlocks/HttpClient/Bigstick.BuildingBlocks.HttpClient.csproj", "BuildingBlocks/HttpClient/"]
COPY ["BuildingBlocks/Infrastructure/Bigstick.BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/Infrastructure/"]
RUN dotnet restore "API/Ferreteria.GestionVentas.API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "Ferreteria.GestionVentas.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ferreteria.GestionVentas.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ferreteria.GestionVentas.API.dll"]

RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf
RUN sed -i 's/SECLEVEL=2/SECLEVEL=1/g' /etc/ssl/openssl.cnf