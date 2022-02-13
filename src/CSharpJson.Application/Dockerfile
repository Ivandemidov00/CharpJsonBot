FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CSharpJson.Application/CSharpJson.Application.csproj", "CSharpJson.Application/"]
RUN dotnet restore "src/CSharpJson.Application/CSharpJson.Application.csproj"
COPY . .
WORKDIR "/src/CSharpJson.Application"
RUN dotnet build "CSharpJson.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpJson.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpJson.Application.dll"]
