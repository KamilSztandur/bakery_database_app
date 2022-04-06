FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BakeryManager.WebAPI/BakeryManager.WebAPI.csproj", "BakeryManager.WebAPI/"]
RUN dotnet restore "BakeryManager.WebAPI/BakeryManager.WebAPI.csproj"
COPY . .
WORKDIR "/src/BakeryManager.WebAPI"
RUN dotnet build "BakeryManager.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BakeryManager.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BakeryManager.WebAPI.dll"]
