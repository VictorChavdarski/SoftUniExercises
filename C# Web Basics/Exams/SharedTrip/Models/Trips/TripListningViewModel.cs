namespace SharedTrip.Models.Trips
{
    public class TripListningViewModel
    {
        public string Id { get; set; }

        public string StartPoint { get; init; }

        public string EndPoint { get; init; }

        public string DepartureTime { get; init; }

        public string ImagePath { get; init; }

        public int Seats { get; init; }
    }
}
