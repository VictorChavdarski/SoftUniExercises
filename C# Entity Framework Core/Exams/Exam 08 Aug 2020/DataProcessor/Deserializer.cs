namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		public const string ErrorMessage = "Invalid Data";

		public const string SuccessfullyImportedGame = "Added {0} ({1}) with {2} tags";

		public const string SuccessfullyImportedUser = "Imported {0} with {1} cards";

		public const string SuccessfullyImportedPurchase = "Imported {0} for {1}";

		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var sb = new StringBuilder();

			ImportGameDto[] gameDtos = JsonConvert.DeserializeObject<ImportGameDto[]>(jsonString);

			var games = new List<Game>();
			var developers = new List<Developer>();
			var genres = new List<Genre>();
			var tags = new List<Tag>();

            foreach (var gameDto in gameDtos)
            {
                if (!IsValid(gameDto))
                {
					sb.AppendLine(ErrorMessage);
					continue;
                }

				DateTime releaseDate;

				bool isReleaseDateValid = DateTime.TryParseExact(gameDto.ReleaseDate, "yyyy-MM-dd",
					CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);

                if (!isReleaseDateValid)
                {
					sb.AppendLine(ErrorMessage);
					continue;
                }

                if (gameDto.Tags.Length == 0)
                {
					sb.AppendLine(ErrorMessage);
					continue;
                }

				Game game = new Game()
				{
					Name = gameDto.Name,
					Price = gameDto.Price,
					ReleaseDate = releaseDate
				};

				var gameDev = developers.FirstOrDefault(d => d.Name == gameDto.Developer);

                if (gameDev == null)
                {
					var newGameDev = new Developer()
					{
						Name = gameDto.Developer
					};
					developers.Add(newGameDev);

					game.Developer = newGameDev;
				}
				else
				{
					game.Developer = gameDev;
				}

				Genre gameGenre = genres
				   .FirstOrDefault(g => g.Name == gameDto.Genre);

				if (gameGenre == null)
				{
					Genre newGenre = new Genre()
					{
						Name = gameDto.Genre
					};

					genres.Add(newGenre);
					game.Genre = newGenre;
				}
				else
				{
					game.Genre = gameGenre;
				}

				foreach (string tagName in gameDto.Tags)
				{
					if (String.IsNullOrEmpty(tagName))
					{
						continue;
					}

					Tag gameTag = tags
						.FirstOrDefault(t => t.Name == tagName);

					if (gameTag == null)
					{
						Tag newGameTag = new Tag()
						{
							Name = tagName
						};

						tags.Add(newGameTag);
						game.GameTags.Add(new GameTag()
						{
							Game = game,
							Tag = newGameTag
						});
					}
					else
					{
						game.GameTags.Add(new GameTag()
						{
							Game = game,
							Tag = gameTag
						});
					}
				}

				if (game.GameTags.Count == 0)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				games.Add(game);
				sb.AppendLine(String.Format(SuccessfullyImportedGame, game.Name, game.Genre.Name, game.GameTags.Count));
			}

			context.Games.AddRange(games);
			context.SaveChanges();

			return sb.ToString().TrimEnd();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var sb = new StringBuilder();

			ImportUserDto[] userDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(jsonString);

			var users = new List<User>();

            foreach (var userDto in userDtos)
            {
                if (!IsValid(userDto))
                {
					sb.AppendLine(ErrorMessage);
					continue;
                }

				var userCards = new List<Card>();

				bool areAllCardsValid = true;
				foreach (var cardDto in userDto.Cards)
				{
					if (!IsValid(cardDto))
					{
						areAllCardsValid = false;
						break;
					}

					Object cardTypeRes;
					bool isCardTypeValid = Enum.TryParse(typeof(CardType), cardDto.Type, out cardTypeRes);

					if (!isCardTypeValid)
					{
						areAllCardsValid = false;
						break;
					}

					CardType cardType = (CardType)cardTypeRes;

					userCards.Add(new Card()
					{
						Number = cardDto.Number,
						Cvc = cardDto.Cvc,
						Type = cardType
					});
				}


				if (!areAllCardsValid)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				if (userCards.Count == 0)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				User user = new User()
				{
					Username = userDto.Username,
					FullName = userDto.FullName,
					Email = userDto.Email,
					Age = userDto.Age,
					Cards = userCards
				};

				users.Add(user);
				sb.AppendLine(String.Format(SuccessfullyImportedUser, user.Username, user.Cards.Count));
			}

			context.Users.AddRange(users);
			context.SaveChanges();

			return sb.ToString().TrimEnd();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var sb = new StringBuilder();

			var xmlSerializer = new XmlSerializer(typeof(ImportPurchaseDto[]), new XmlRootAttribute("Purchases"));

			using StringReader stringReader = new StringReader(xmlString);

			ImportPurchaseDto[] purchaseDtos = (ImportPurchaseDto[])xmlSerializer.Deserialize(stringReader);

			var purchases = new List<Purchase>();

            foreach (var purchaseDto in purchaseDtos)
            {
                if (!IsValid(purchaseDto))
                {
					sb.AppendLine(ErrorMessage);
					continue;
                }

				object purchaseTypeObj;
				bool isPurchaseTypeValid =
					Enum.TryParse(typeof(PurchaseType), purchaseDto.PurchaseType, out purchaseTypeObj);

				if (!isPurchaseTypeValid)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				PurchaseType purchaseType = (PurchaseType)purchaseTypeObj;

				DateTime date;
				bool isDateValid = DateTime.TryParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm",
					CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

				if (!isDateValid)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				Card card = context
					.Cards
					.FirstOrDefault(c => c.Number == purchaseDto.CardNumber);

				if (card == null)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				Game game = context
					.Games
					.FirstOrDefault(g => g.Name == purchaseDto.GameTitle);

				if (game == null)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				Purchase p = new Purchase()
				{
					Type = purchaseType,
					Date = date,
					ProductKey = purchaseDto.Key,
					Game = game,
					Card = card
				};

				purchases.Add(p);
				sb.AppendLine(String.Format(SuccessfullyImportedPurchase, p.Game.Name, p.Card.User.Username));

			}

			context.Purchases.AddRange(purchases);
			context.SaveChanges();

			return sb.ToString().TrimEnd();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}