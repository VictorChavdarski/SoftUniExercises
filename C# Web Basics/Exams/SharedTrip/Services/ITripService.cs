namespace SharedTrip.Services
{
    using SharedTrip.Models.Trips;

    public interface ITripService
    {
        TripDetailsListningModel GetDetails(string id);

        bool CanUserJoinTrip(string userId, string tripId);

        void AddUserToTrip(string userId, string tripId);
    }
}
