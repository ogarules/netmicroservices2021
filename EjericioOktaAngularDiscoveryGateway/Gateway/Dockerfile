FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["Gateway/Gateway.csproj", "Gateway/"]
RUN dotnet restore "Gateway/Gateway.csproj"
COPY . .
WORKDIR "/src/Gateway"
RUN dotnet build "Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
VOLUME ${APPDATA}/Microsoft/UserSecrets/:/root/.microsoft/usersecrets:ro
VOLUME ${APPDATA}/ASP.NET/Https:/root/.aspnet/https:ro
#VOLUME ${USERPROFILE}/.aspnet/Https:/root/.aspnet/https:ro
ENTRYPOINT ["dotnet", "Gateway.dll"]
