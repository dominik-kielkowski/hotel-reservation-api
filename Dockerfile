FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["HotelReservation.API/HotelReservation.API.csproj", "HotelReservation.API/"]
COPY ["HotelReservation.Infrastructure/HotelReservation.Infrastructure.csproj", "HotelReservation.Infrastructure/"]
COPY ["HotelReservation.Application/HotelReservation.Application.csproj", "HotelReservation.Application/"]
COPY ["HotelReservation.Core/HotelReservation.Core.csproj", "HotelReservation.Core/"]
RUN dotnet restore "HotelReservation.API/HotelReservation.API.csproj"

COPY . .

WORKDIR "/src/HotelReservation.API"
RUN dotnet build "HotelReservation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HotelReservation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "HotelReservation.API.dll"]