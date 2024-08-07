namespace Application.Hotels.Rooms.MakeReservation
{
    public record ReservationConfirmedEvent(int ReservationId, Guid UserId, int RoomId, DateTime Begin, DateTime End);
}
