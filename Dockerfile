FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /app

COPY . ./
WORKDIR /app/server

RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build-env /app/server/out .
ENV ASPNETCORE_URLS http://*:5000

WORKDIR /app
ENTRYPOINT ["dotnet", "CryptobotUi.Server.dll"]

EXPOSE 5000
