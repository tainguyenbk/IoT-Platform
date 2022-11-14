# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY ..
RUN dotnet restore "./IoTPlatform.API/IoTPlatform.API.csproj" --disable-parallel
RUN dotnet publish "./IoTPlatform.API/IoTPlatform.API.csproj" -c release -o /app --no-release

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "IoTPlatform.API"]