FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
 
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/AuctionWebApp/AuctionWebApp.csproj", "src/AuctionWebApp/"]
COPY ["/DAL/DAL.csproj", "src/DAL/"]
COPY ["/BLL/BLL.csproj", "src/BLL/"]
RUN dotnet restore "src/AuctionWebApp/AuctionWebApp.csproj"
COPY . .
WORKDIR "/src/AuctionWebApp"
RUN dotnet build "AuctionWebApp.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "AuctionWebApp.csproj" -c Release -o /app/publish
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuctionWebApp.dll"]