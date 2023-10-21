#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microscore.api/microscore.api.csproj", "microscore.api/"]
COPY ["microscore/microscore.application/microscore.application.csproj", "microscore/microscore.application/"]
COPY ["microscore/microscore.domain/microscore.domain.csproj", "microscore/microscore.domain/"]
COPY ["microscore/microscore.infrastructure/microscore.infrastructure.csproj", "microscore/microscore.infrastructure/"]
RUN dotnet restore "microscore.api/microscore.api.csproj"
COPY . .
WORKDIR "/src/microscore.api"
RUN dotnet build "microscore.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "microscore.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

COPY ["microscore.api/certificado.pfx", "microscore.api/"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "microscore.api.dll"]