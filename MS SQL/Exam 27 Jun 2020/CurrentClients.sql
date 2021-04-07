SELECT c.FirstName + ' ' + c.LastName AS Client,
	DATEDIFF(DAY, j.IssueDate, '04/24/2017') AS days,
	j.Status
	FROM Jobs AS j
	JOIN Clients AS c ON c.ClientId = j.ClientId
	WHERE j.Status != 'Finished'
ORDER BY days DESC, c.ClientId ASC