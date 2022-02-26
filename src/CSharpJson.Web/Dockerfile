FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CSharpJson.Web/CSharpJson.Web.csproj", "CSharpJson.Web/"]
RUN dotnet restore "src/CSharpJson.Web/CSharpJson.Web.csproj"
COPY . .
WORKDIR "/src/CSharpJson.Web"
RUN dotnet build "CSharpJson.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpJson.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpJson.Web.dll"]
