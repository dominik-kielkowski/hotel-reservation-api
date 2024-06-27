using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hotels.Rooms.GetRoomById
{
    public sealed record GetRoomByIdQuery(int roomId) : IRequest<RoomDto>;
}
