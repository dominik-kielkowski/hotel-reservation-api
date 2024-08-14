namespace Application.Hotels.Rooms.MakeReservation
{
    public record ReservationConfirmedEvent(int ReservationId, Guid UserId, int RoomId, string UserEmail, DateTime Begin, DateTime End);
}
