using Core.Entities.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hotels.Rooms.UpdateRoom
{
    public sealed record UpdateRoomCommand(Room room) : IRequest;
}
