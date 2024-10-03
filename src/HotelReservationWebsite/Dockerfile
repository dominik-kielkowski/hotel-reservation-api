# Base image with ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image with the .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["HotelReservationWebsite/HotelReservation.API.csproj", "HotelReservationWebsite/"]
COPY ["Infrastructure/HotelReservation.Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/HotelReservation.Application.csproj", "Application/"]
COPY ["Core/HotelReservation.Core.csproj", "Core/"]
RUN dotnet restore "HotelReservationWebsite/HotelReservation.API.csproj"

# Copy the rest of the application source code
COPY . .

# Build the application
WORKDIR "/src/HotelReservationWebsite"
RUN dotnet build "./HotelReservation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./HotelReservation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelReservation.API.dll"]

# Optional: Health check
HEALTHCHECK CMD curl --fail http://localhost:80/ || exit 1
