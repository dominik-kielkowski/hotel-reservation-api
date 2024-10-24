FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY HotelReservationAPI.sln ./
COPY src/HotelReservationWebsite/HotelReservation.API.csproj src/HotelReservationWebsite/
COPY src/Core/HotelReservation.Core.csproj src/Core/
COPY src/Application/HotelReservation.Application.csproj src/Application/
COPY src/Infrastructure/HotelReservation.Infrastructure.csproj src/Infrastructure/
COPY tests/HotelReservation.Tests/HotelReservation.Tests.csproj tests/HotelReservation.Tests/

RUN dotnet restore HotelReservationAPI.sln

COPY . .

RUN dotnet publish HotelReservationAPI.sln -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "HotelReservation.API.dll"]