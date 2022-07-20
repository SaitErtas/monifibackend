#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/MonifiBackend.API/MonifiBackend.API.csproj", "Presentation/MonifiBackend.API/"]
COPY ["Modules/WalletModule/MonifiBackend.WalletModule.Application/MonifiBackend.WalletModule.Application.csproj", "Modules/WalletModule/MonifiBackend.WalletModule.Application/"]
COPY ["Modules/WalletModule/MonifiBackend.WalletModule.Domain/MonifiBackend.WalletModule.Domain.csproj", "Modules/WalletModule/MonifiBackend.WalletModule.Domain/"]
COPY ["Modules/Core/MonifiBackend.Core.Domain/MonifiBackend.Core.Domain.csproj", "Modules/Core/MonifiBackend.Core.Domain/"]
COPY ["Modules/Core/MonifiBackend.Core.Application/MonifiBackend.Core.Application.csproj", "Modules/Core/MonifiBackend.Core.Application/"]
COPY ["Modules/UserModule/MonifiBackend.UserModule.Infrastructure/MonifiBackend.UserModule.Infrastructure.csproj", "Modules/UserModule/MonifiBackend.UserModule.Infrastructure/"]
COPY ["Modules/UserModule/MonifiBackend.UserModule.Domain/MonifiBackend.UserModule.Domain.csproj", "Modules/UserModule/MonifiBackend.UserModule.Domain/"]
COPY ["Modules/Core/MonifiBackend.Core.Infrastructure/MonifiBackend.Core.Infrastructure.csproj", "Modules/Core/MonifiBackend.Core.Infrastructure/"]
COPY ["Framework/MonifiBackend.Data/MonifiBackend.Data.csproj", "Framework/MonifiBackend.Data/"]
COPY ["Modules/UserModule/MonifiBackend.UserModule.Application/MonifiBackend.UserModule.Application.csproj", "Modules/UserModule/MonifiBackend.UserModule.Application/"]
COPY ["Modules/WalletModule/MonifiBackend.WalletModule.Infrastructure/MonifiBackend.WalletModule.Infrastructure.csproj", "Modules/WalletModule/MonifiBackend.WalletModule.Infrastructure/"]
COPY ["Modules/PackageModule/MonifiBackend.PackageModule.Infrastructure/MonifiBackend.PackageModule.Infrastructure.csproj", "Modules/PackageModule/MonifiBackend.PackageModule.Infrastructure/"]
COPY ["Modules/PackageModule/MonifiBackend.PackageModule.Domain/MonifiBackend.PackageModule.Domain.csproj", "Modules/PackageModule/MonifiBackend.PackageModule.Domain/"]
COPY ["Modules/PackageModule/MonifiBackend.PackageModule.Application/MonifiBackend.PackageModule.Application.csproj", "Modules/PackageModule/MonifiBackend.PackageModule.Application/"]
RUN dotnet restore "Presentation/MonifiBackend.API/MonifiBackend.API.csproj"
COPY . .
WORKDIR "Presentation/MonifiBackend.API"
RUN dotnet build "MonifiBackend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonifiBackend.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonifiBackend.API.dll"]