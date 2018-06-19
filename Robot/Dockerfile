FROM microsoft/aspnetcore:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.1 AS build
WORKDIR /src
COPY Robot/Robot.csproj Robot/
RUN dotnet restore Robot/Robot.csproj
COPY . .
WORKDIR /src/Robot
RUN dotnet build Robot.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Robot.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Robot.dll"]
