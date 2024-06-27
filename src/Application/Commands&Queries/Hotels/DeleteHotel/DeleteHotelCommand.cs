using Core.Entities.Hotels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands_Queries.Hotels
{
    public record DeleteHotelCommand(Hotel Hotel) : IRequest;
}
