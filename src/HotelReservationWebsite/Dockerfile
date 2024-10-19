FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/HotelReservationWebsite/HotelReservation.API.csproj", "src/HotelReservationWebsite/"]
COPY ["src/Infrastructure/HotelReservation.Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Application/HotelReservation.Application.csproj", "src/Application/"]
COPY ["src/Core/HotelReservation.Core.csproj", "src/Core/"]
RUN dotnet restore "./src/HotelReservationWebsite/HotelReservation.API.csproj"
COPY . .
WORKDIR "/src/HotelReservationWebsite"
RUN dotnet build "./HotelReservation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HotelReservation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelReservation.API.dll"]