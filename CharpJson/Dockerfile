FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CharpJson/CharpJson.csproj", "CharpJson/"]
RUN dotnet restore "CharpJson/CharpJson.csproj"
COPY . .
WORKDIR "/src/CharpJson"
RUN dotnet build "CharpJson.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CharpJson.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENTRYPOINT ["dotnet", "CharpJson.dll"]
