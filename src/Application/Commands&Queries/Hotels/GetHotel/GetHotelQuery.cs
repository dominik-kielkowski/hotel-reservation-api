using Application.Common;
using Application.Hotels;
using Core.Entities.Hotels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands_Queries.Hotels.GetHotel
{
    public record GetHotelQuery(int Id) : IRequest<HotelDto>;
}
