SELECT J.JobId , ISNULL(SUM(p.Price * op.Quantity),0) AS TotalOrder
  FROM Jobs AS j
  LEFT JOIN Orders AS o ON o.JobId = j.JobId
  LEFT JOIN OrderParts AS op ON op.OrderId = o.OrderId
  LEFT JOIN Parts AS p ON p.PartId = op.PartId
  WHERE Status = 'Finished'
  GROUP BY J.JobId
ORDER BY TotalOrder DESC, J.JobId ASC