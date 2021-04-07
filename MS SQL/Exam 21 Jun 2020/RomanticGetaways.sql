SELECT AccountId as Id, Email AS Email,
	ac.Name AS City, COUNT(*) AS Trips
	FROM AccountsTrips AS at
	JOIN Accounts AS a ON at.AccountId = a.Id 
	JOIN Cities AS ac ON a.CityId = ac.Id
	JOIN Trips AS t ON at.TripId = t.Id
	JOIN Rooms AS r ON t.RoomId = r.Id
	JOIN Hotels AS h ON r.HotelId = h.Id
	JOIN Cities AS hc ON h.CityId = hc.Id
	WHERE hc.Id = ac.Id
	GROUP BY AccountId, Email, ac.Name
	ORDER BY Trips DESC, AccountId ASC