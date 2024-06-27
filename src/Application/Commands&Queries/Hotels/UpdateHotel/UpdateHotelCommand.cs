using Core.Entities.Hotels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands_Queries.Hotels.UpdateHotel
{
    public record UpdateHotelCommand(Hotel Hotel) : IRequest;
}
