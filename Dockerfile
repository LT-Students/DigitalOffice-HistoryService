FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /app

COPY . ./
RUN dotnet restore

COPY . ./https://github.com/LT-Students/DigitalOffice-HistoryService/blob/develop/Dockerfile
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "LT.DigitalOffice.HistoryService.dll"]
