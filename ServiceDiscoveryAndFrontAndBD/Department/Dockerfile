FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["Department/Department.csproj", "Department/"]
RUN dotnet restore "Department/Department.csproj"
COPY . .
WORKDIR "/src/Department"
RUN dotnet build "Department.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Department.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Department.dll"]
