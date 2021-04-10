SELECT   ISNULL((e.FirstName + ' ' + e.LastName), 'None') AS Employee,
  ISNULl(d.Name, 'None') AS Department,
  ISNULL(c.Name, 'None') AS Category,
  ISNULL(r.Description, 'None') AS [Description],
  ISNULL(FORMAT(r.OpenDate,'dd.MM.yyyy'), 'None') AS [OpenDate],
  ISNULL(s.Label, 'None') AS [Status],
  ISNULL(u.Name, 'None') AS [User]
  FROM Reports AS r
  LEFT JOIN Employees AS e ON e.Id = r.EmployeeId
  LEFT JOIN Departments AS d ON d.Id = e.DepartmentId
  JOIN Categories AS c ON c.Id = r.CategoryId
  JOIN Users AS u ON u.Id = r.UserId
  JOIN Status AS s ON s.Id = r.StatusId 
  GROUP BY (e.FirstName + ' ' + e.LastName),d.Name,c.Name,r.Description,r.OpenDate,s.Label,u.Name,e.FirstName,e.LastName
 ORDER BY e.FirstName DESC, 
  e.LastName DESC, d.Name ASC, 
  c.Name ASC, r.Description ASC, 
  r.OpenDate ASC, s.Label ASC, 
  u.Name ASC

