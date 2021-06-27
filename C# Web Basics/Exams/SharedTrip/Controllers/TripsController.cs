namespace SharedTrip.Controllers
{
    using System;
    using System.Linq;
    using System.Globalization;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using SharedTrip.Data;
    using SharedTrip.Services;
    using SharedTrip.Models.Trips;
    using SharedTrip.Data.Models;


    public class TripsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IValidator validator;
        private readonly ITripService tripService;

        public TripsController(
            ApplicationDbContext data,
            IValidator validator,
            ITripService tripService)
        {
            this.validator = validator;
            this.data = data;
            this.tripService = tripService;
        }

        public HttpResponse All()
        {
            var repositories = this.data
                .Trips
                .Select(t => new TripListningViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("R"),
                    Seats = t.Seats
                })
                .ToArray();

            return View(repositories);
        }

        [Authorize]
        public HttpResponse Add() => View();

        [HttpPost]
        [Authorize]
        public HttpResponse Add(TripFormModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return this.Error(modelErrors);
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = DateTime.ParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                ImagePath = model.ImagePath,
                Seats = model.Seats,
                Description = model.Description
            };

            this.data.Trips.Add(trip);

            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.tripService.CanUserJoinTrip(this.User.Id, tripId))
            {
                return this.Error("No available seats!");
            }

            var userId = this.User.Id;
            this.tripService.AddUserToTrip(userId, tripId);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            var trip = this.tripService.GetDetails(tripId);

            return this.View(trip);
        }
    }
}

