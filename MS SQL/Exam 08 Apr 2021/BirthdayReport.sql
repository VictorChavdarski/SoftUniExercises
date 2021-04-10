SELECT u.Username, c.Name 
	FROM Reports AS r
	JOIN Users AS u ON u.Id = r.UserId
	JOIN Categories AS c ON c.Id = r.CategoryId
	WHERE month(u.Birthdate) = month(r.OpenDate)
      and day(u.Birthdate) = day(r.OpenDate)
GROUP BY u.Username, c.Name
ORDER BY u.Username ASC, c.Name ASC

