FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["EmployeeService/EmployeeService.csproj", "EmployeeService/"]
RUN dotnet restore "EmployeeService/EmployeeService.csproj"
COPY . .
WORKDIR "/src/EmployeeService"
RUN dotnet build "EmployeeService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeService.dll"]
