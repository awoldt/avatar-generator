FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["avatargeneratorV2.csproj", "."]
RUN dotnet restore "./avatargeneratorV2.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "avatargeneratorV2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "avatargeneratorV2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "avatargeneratorV2.dll"]