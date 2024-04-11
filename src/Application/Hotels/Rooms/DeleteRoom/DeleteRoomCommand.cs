using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hotels.Rooms.DeleteRoom
{
    public sealed record DeleteRoomCommand(int roomId) : IRequest;
}
