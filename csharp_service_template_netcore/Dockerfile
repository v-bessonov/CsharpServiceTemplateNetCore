﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 9001
EXPOSE 443

# Ensure we listen on any IP Address 
#ENV ASPNETCORE_URLS=http://+:9001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WorkerTest/WorkerTest.csproj", "WorkerTest/"]
RUN dotnet restore "WorkerTest/WorkerTest.csproj"
COPY . .
WORKDIR "/src/WorkerTest"
RUN dotnet build "WorkerTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkerTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerTest.dll"]