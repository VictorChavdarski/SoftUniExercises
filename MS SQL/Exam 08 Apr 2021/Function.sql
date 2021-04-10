CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME)
RETURNS INT
AS
BEGIN
	IF(@StartDate IS NULL OR @EndDate IS NULL)
	BEGIN
	 RETURN 0;
	END

 DECLARE @Hours INT = datediff(hour, @StartDate, @EndDate);
 RETURN @Hours;

END