CREATE PROCEDURE usp_PlaceOrder
(
	@jobId INT,
	@serialNumber VARCHAR(50),
	@qty INT
)
AS

DECLARE @status VARCHAR(10) = (SELECT Status FROM Jobs WHERE JobId = @jobId)
DECLARE @partId VARCHAR(10) = (SELECT * FROM Parts WHERE SerialNumber = @serialNumber)

IF(@qty <= 0)
THROW 50012, 'Part quantity must be more than zero!', 1
ELSE IF(@status = 'Finished')
THROW 50011, 'This job is not active!', 1
ELSE IF(@status IS NULL)
THROW 50013, 'Job not found!', 1
ELSE IF(@partId IS NULL)
THROW 50014, 'Part not found!', 1

DECLARE @orderId INT = (SELECT OrderId FROM Orders WHERE JobId= @jobId)
IF(@orderId IS NULL)
BEGIN
INSERT INTO Orders (JobId, IssueDate) VALUES
(@jobId, NULL)

SET @orderId =(SELECT OrderId FROM Orders WHERE JobId= @jobId)

INSERT INTO OrderParts (OrderId, PartId) VALUES
(@orderId, @partId)
END
ELSE 
BEGIN
DECLARE @issueDate DATE = (SELECT * Orders WHERE OrderId = @orderId)

IF(@issueDate IS NULL)
INSERT INTO OrderParts (OrderId, PartId, Quantity) VALUES 
(@orderId, @partId, @qty)

ELSE
	UPDATE OrderParts
	SET Quantity += @qty
	WHERE OrderId= @orderId AND PartId = @partId
END

DECLARE @err_msg AS NVARCHAR(MAX)
BEGIN TRY

BEGIN CATCH
	SET @err_msg = ERROR_MESSAGE();
	SELECT @err_msg
END CATCH