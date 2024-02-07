using System.Collections.Generic;
using Core.Entities;
using HotelReservationWebsite.Dtos;

namespace HotelReservationWebsite.Extensions
{
    public static class HotelExtensions
    {
        public static HotelDto ToDto(this Hotel hotel)
        {
            if (hotel == null)
                return null;

            return new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Description = hotel.Description,
                Country = hotel.Address.Country,
                City = hotel.Address.City,
                Street = hotel.Address.Street,
                BuildingNumber = hotel.Address.BuildingNumber,
                PostalCode = hotel.Address.PostalCode,
                Rooms = hotel.Rooms.Select(room => room.ToDto()).ToList()
            };
        }
    }
}
