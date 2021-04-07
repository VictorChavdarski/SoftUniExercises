CREATE FUNCTION udf_GetCost (@jobId INT)
RETURNS DECIMAL(15,2)
AS
BEGIN

DECLARE @sum DECIMAL(15,2)

SET @sum =(Select 
        SUM(p.Price * op.Quantity) AS TotalCost 
	     FROM Jobs AS j 
	     JOIN Orders AS o ON o.JobId = j.JobId
	     JOIN OrderParts AS op ON op.OrderId = o.OrderId
	     JOIN Parts AS p ON p.PartId = op.PartId
	WHERE j.JobId = @jobId );
IF(@sum = 0)
 BEGIN
   SET @sum = 0;
 END
 RETURN @sum
END

