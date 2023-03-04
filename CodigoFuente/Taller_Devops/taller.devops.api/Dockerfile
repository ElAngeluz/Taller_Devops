#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["taller.devops.api/taller.devops.api.csproj", "taller.devops.api/"]
RUN dotnet restore "taller.devops.api/taller.devops.api.csproj"
COPY . .
WORKDIR "/src/taller.devops.api"
RUN dotnet publish "taller.devops.api/taller.devops.api.csproj" -c Release -o /app/publish --no-restore --self-contained true /p:PublishTrimmed=true /p:PublishSingleFile=true

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "taller.devops.api.dll"]
