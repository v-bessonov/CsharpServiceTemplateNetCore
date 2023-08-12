FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 9001
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["csharp_service_template_netcore/csharp_service_template_netcore.csproj", "csharp_service_template_netcore/"]
RUN dotnet restore "csharp_service_template_netcore/csharp_service_template_netcore.csproj"
COPY . .
WORKDIR "/src/csharp_service_template_netcore"
RUN dotnet build "csharp_service_template_netcore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "csharp_service_template_netcore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "csharp_service_template_netcore.dll"]
