namespace SharedTrip.Services
{
    using System.Linq;

    using SharedTrip.Data;
    using SharedTrip.Data.Models;
    using SharedTrip.Models.Trips;

    public class TripService : ITripService
    {
        private readonly ApplicationDbContext data;

        public TripService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void AddUserToTrip(string userId, string tripId)
        {
            var userIsInTrip = this.data.UserTrips
                .Any(x => x.UserId == userId && x.TripId == tripId);

            if (userIsInTrip)
            {
                return;
            }

            var userInTrip = new UserTrip
            {
                UserId = userId,
                TripId = tripId,
            };

            this.data.UserTrips.Add(userInTrip);
            this.data.SaveChanges();
        }

        public bool CanUserJoinTrip(string userId, string tripId)
        {
            var trip = this.data
                .Trips
                .Where(x => x.Id == tripId)
                .Select(x => new
                {
                    x.Seats,
                    TakenSeats = x.UserTrips.Count()
                })
                .FirstOrDefault();

            var freeSeats = trip.Seats - trip.TakenSeats;

            if (freeSeats <= 0)
            {
                return false;
            }

            return true;
        }

        public TripDetailsListningModel GetDetails(string id)
        {
            var trip = this.data
                 .Trips
                 .Where(x => x.Id == id)
                 .Select(x => new TripDetailsListningModel
                 {
                     StartPoint = x.StartPoint,
                     EndPoint = x.EndPoint,
                     DepartureTime = x.DepartureTime.ToString("R"),
                     Seats = x.Seats,
                     Description = x.Description,

                 })
                 .FirstOrDefault();

            return trip;
        }
    }
}
