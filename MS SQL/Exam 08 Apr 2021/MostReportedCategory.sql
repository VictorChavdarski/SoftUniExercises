SELECT TOP(5) c.Name, COUNT(r.Id) AS Numbers
 FROM Categories AS c
 JOIN Reports AS r ON r.CategoryId = c.Id
 GROUP BY c.Name
 ORDER BY Numbers DESC, c.Name ASC