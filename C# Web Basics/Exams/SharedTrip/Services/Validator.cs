namespace SharedTrip.Services
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using SharedTrip.Models.Trips;
    using SharedTrip.Models.Users;

    using static Data.DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UsernameMinLength || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UsernameMinLength} and {DefaultMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < PasswordMinLength || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {DefaultMaxLength} characters long.");
            }

            if (model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            return errors;
        }

        public ICollection<string> ValidateTrip(TripFormModel model)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(model.StartPoint))
            {
                errors.Add("Start point is required!");
            }

            if (string.IsNullOrEmpty(model.EndPoint))
            {
                errors.Add("End point is required!");
            }

            if (string.IsNullOrEmpty(model.Description) || model.Description.Length > DescriptionMaxLength)
            {
                errors.Add("Description has max lenth 80 and it is required!");
            }

            if (model.Seats < TripSeatsMinNumber || model.Seats > TripSeatsmaxNumber)
            {
                errors.Add($"Trip seats must be between {TripSeatsMinNumber} and {TripSeatsmaxNumber} seats.");
            }

            if (!DateTime.TryParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture,DateTimeStyles.None, out _))
            {
                errors.Add("Datetime format is incorrect!");
            }

            return errors;
        }
    }
}
