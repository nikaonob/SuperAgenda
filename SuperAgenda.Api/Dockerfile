FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY SuperAgenda.Api.csproj .
RUN dotnet restore SuperAgenda.Api.csproj

COPY . .
RUN dotnet publish SuperAgenda.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SuperAgenda.Api.dll"]
