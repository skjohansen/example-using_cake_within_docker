FROM cakebuild/cake:sdk-7.0 AS build
WORKDIR /src

COPY "WeatherApi/build.cake" .
COPY "WeatherApi/WeatherApi.csproj" .

RUN dotnet cake --target="Restore NuGet packages"
COPY WeatherApi/ .
RUN dotnet cake --target="Build API" --exclusive

FROM build AS publish
RUN dotnet cake --target="Publish API" --exclusive

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /final
COPY --from=publish /out .

ENTRYPOINT ["dotnet", "WeatherApi.dll"]