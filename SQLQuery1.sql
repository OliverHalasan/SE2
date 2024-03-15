select * from ClubMember

sp_helptext 'MemberLogin'

CREATE OR ALTER PROCEDURE AddClubMember (@FirstName  VARCHAR(255) NULL,  
          @LastName  VARCHAR(255) NULL,  
          @Address  VARCHAR(100) NULL,  
          @PostalCode  VARCHAR (10) NULL,  
          @Phone   VARCHAR(10)  NULL,  
          @AlterPhone  VARCHAR(10)  NULL,  
          @CompanyName VARCHAR(100) NULL,
		  @Occupation Varchar (100) NULL,
          @AddressLine1 VARCHAR(100) NULL,  
          @AddressLine2 VARCHAR(100) NULL,  
          @Email   VARCHAR(255) NULL,  
          @DateOfBirth DATE,  
          @MembershipTypeID INT,  
          @PasswordHash VARCHAR (255) NULL,  
          @Role   VARCHAR (10) NULL,  
          @Approved  VARCHAR (1) = 'W')  
           
AS  
 DECLARE @ReturnCode INT  
 SET @ReturnCode = 1  
  
 IF @FirstName IS NULL  
  RAISERROR('FirstName - Required Parameter: @FirstName is missing.', 16, 1);  
 ELSE  
 IF @LastName IS NULL   
  RAISERROR('LastName - Required Parameter: @LastName is missing.', 16, 1);  
 ELSE  
 IF @Address IS NULL   
  RAISERROR('Address - Required Parameter: @Address is missing.', 16, 1);  
 ELSE  
 IF @PostalCode IS NULL  
  RAISERROR('PostalCode - Required Parameter: @PostalCode is missing.', 16, 1);  
 ELSE  
 IF @Phone IS NULL  
  RAISERROR('Phone - Required Parameter: @Phone is missing.', 16, 1);  
 ELSE  
 IF @DateOfBirth IS NULL  
  RAISERROR('DateOfBirth - Required Parameter: @DateOfBirth is missing.', 16, 1);  
 ELSE  
 IF @MembershipTypeID IS NULL  
  RAISERROR('MembershipTypeID - Required Parameter: @MembershipTypeID is missing.', 16, 1);  
 ELSE  
 IF @PasswordHash IS NULL  
  RAISERROR('PasswordHash - Required Parameter: @PasswordHash is missing.', 16, 1);  
 ELSE  
 IF @Role IS NULL  
  RAISERROR('Role - Required Parameter: @Role is missing.', 16, 1);  
 ELSE  
 BEGIN  
 INSERT INTO ClubMember (FirstName, LastName, Address, PostalCode, Phone, AlterPhone, Occupation, CompanyName, AddressLine1, AddressLine2, Email, DateOfBirth, MembershipTypeID, PasswordHash, Role, Approved)  
 VALUES (@FirstName, @LastName, @Address, @PostalCode, @Phone, @AlterPhone, @Occupation, @CompanyName, @AddressLine1, @AddressLine2, @Email, @DateOfBirth, @MembershipTypeID, @PasswordHash, @Role, @Approved);  
  
  
 IF @@ERROR = 0  
  SET @ReturnCode = 0  
 ELSE  
  RAISERROR('ClubMember - Insert Error: ClubMember ',16,1)  
 END  
   
Return @ReturnCode

exec AddClubMember 'name','last','asdf','T9E0J8','5879886636'

ALTER TABLE ClubMember
ALTER COLUMN Phone VARCHAR(10) NOT NULL; -- Assuming AlterPhone is a VARCHAR column


create TABLE StandingTeeTime (
    StandingID INT PRIMARY KEY IDENTITY,
    MemberNumber1 VARCHAR(100),
    Name1 VARCHAR(255),
	    MemberNumber2 VARCHAR(100),
    Name2 VARCHAR(255),
	    MemberNumber3 VARCHAR(100),
    Name3 VARCHAR(255),
	    MemberNumber4 VARCHAR(100),
    Name4 VARCHAR(255),
    RequestedTeeTime time,
	RequestedDate date

);

Create OR ALTER Procedure TeeTimeRequest (	@MemberNumber1 Varchar(100) null,
											@Name1 Varchar(255) null,
											@MemberNumber2 Varchar(100) null,
											@Name2 Varchar(255) null,
											@MemberNumber3 Varchar(100) null,
											@Name3 Varchar(255) null,
											@MemberNumber4 Varchar(100) null,
											@Name4 Varchar(255) null,
											@RequestedTeeTime time,
											@RequestedDate date
											)
as
	DECLARE @ReturnCode INT  
	SET @ReturnCode = 1 
	
	IF @MemberNumber1 is null
		RAISERROR('MemberNumber1 - Required Parameter: @MemberNumber1 is missing.', 16,1);
	Else
	If @Name1 is null
		RAISERROR('Name1 - Required Parameter: @Name1 is missing.', 16,1);
	Else
	IF @RequestedTeeTime is null
		RAISERROR('RequestedTeeTime - Required Parameter: @RequestedTeeTime is missing',16,1);
	Else
	If @RequestedDate is null
		RAISERROR('RequestedDate - Required Parameter: @RequestedDate is missing', 16,1);
	ELSE
	       -- Check if the requested tee time is available
        IF EXISTS (
            SELECT 1
            FROM StandingTeeTime
            WHERE RequestedDate = @RequestedDate
            AND RequestedTeeTime = @RequestedTeeTime
        )
        BEGIN
            RAISERROR('Requested tee time is not available.', 16, 1);
        END
        ELSE
	Begin
	Insert into StandingTeeTime (MemberNumber1,Name1,MemberNumber2,Name2,MemberNumber3,Name3,MemberNumber4,Name4,RequestedTeeTime, RequestedDate)
	values (@MemberNumber1,@Name1,@MemberNumber2,@Name2,@MemberNumber3,@Name3,@MemberNumber4,@Name4,@RequestedTeeTime,@RequestedDate);

	 DECLARE @StandingID INT;
            SET @StandingID = SCOPE_IDENTITY();

	INSERT INTO BookingTeeTime (StandingID, MemberID)
	VALUES (@StandingID, (SELECT MemberID FROM ClubMember WHERE MemberID = @MemberNumber1));

	IF @@ERROR = 0
		SET @ReturnCode = 0
	Else
	RAISERROR ('TeeTimeRequest - Insert Error: StandingTeeTime',16,1)
end

return @ReturnCode

EXEC TeeTimeRequest '1', 'asdf', null, null, null, null, null, null, '10:00', '2024-03-15';

select * from StandingTeeTime


create TABLE BookingTeeTime (
   BookTeeTimeID INT PRIMARY KEY IDENTITY,
    StandingID INT,
    MemberID INT,
    FOREIGN KEY (StandingID) REFERENCES StandingTeeTime(StandingID),
    FOREIGN KEY (MemberID) REFERENCES ClubMember(MemberID)
);

ALTER TABLE BookingTeeTime
ALTER COLUMN  BookTeeTimeID INT PRIMARY KEY; 
select * from BookingTeeTime

CREATE OR ALTER PROCEDURE IsTeeTimeAvailable
    @RequestedDate DATE,
    @RequestedTeeTime TIME
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Available BIT = 1;

    -- Check if the requested tee time is available
    IF EXISTS (
        SELECT 1
        FROM StandingTeeTime
        WHERE RequestedDate = @RequestedDate
        AND RequestedTeeTime = @RequestedTeeTime
    )
    BEGIN
        SET @Available = 0;
    END

    -- Return the availability status
    SELECT @Available AS IsAvailable;
END

-- Declare variables for input parameters
DECLARE @RequestedDate DATE = '2024-03-20'; -- Adjust date as needed
DECLARE @RequestedTeeTime TIME = '08:00:00'; -- Adjust time as needed

-- Execute the stored procedure
EXEC IsTeeTimeAvailable @RequestedDate, @RequestedTeeTime;


 CREATE OR ALTER  PROCEDURE MemberLogin (@Email  VARCHAR(255),  
        @PasswordHash VARCHAR (255))  
as  
 Declare @ReturnCode INT  
 Set @ReturnCode = 1  
 IF @Email IS NULL  
  RAISERROR('Email - Required Parameter: @Email',16,1)  
 ELSE  
 IF @PasswordHash IS NULL  
  RAISERROR('PasswordHash - Required Parameter: @PasswordHash',16,1)  
 ELSE  
 BEGIN  
 SELECT Email, PasswordHash,Role,FirstName,LastName,MemberID from ClubMember  
 WHERE Email = @Email AND PasswordHash = @PasswordHash  
 IF @@ERROR = 0  
  SET @ReturnCode = 0  
 ELSE  
  RAISERROR('ClubMember - Login Error: ClubMember Table ',16,1)  
 END  
   
Return @ReturnCode


SELECT *
FROM BookingTeeTime
INNER JOIN StandingTeeTime ON BookingTeeTime.StandingID = StandingTeeTime.StandingID
WHERE MemberID = 6
AND CONCAT(RequestedDate, ' ', RequestedTeeTime) >= CONCAT(CAST(GETDATE() AS DATE), ' ', CAST(GETDATE() AS TIME));

Create or alter procedure TeeTimeList (@MemberID int null)
as
	Declare @ReturnCode INT
	Set @ReturnCode = 1
	IF @MemberID IS NULL
	RAISERROR('MemberID - Required Parameter: @Parameter',16,1)
	Else
	Begin
	
SELECT RequestedDate, RequestedTeeTime,StandingTeeTime.StandingID
FROM BookingTeeTime
INNER JOIN StandingTeeTime ON BookingTeeTime.StandingID = StandingTeeTime.StandingID
WHERE MemberID = @MemberID
AND CONCAT(RequestedDate, ' ', RequestedTeeTime) >= CONCAT(CAST(GETDATE() AS DATE), ' ', CAST(GETDATE() AS TIME))
IF @@ERROR = 0
Set @ReturnCode = 0
Else
	Raiserror('TeeTimeList - List Error: BookingTeeTime Table',16,1)
	End
Return @ReturnCode

exec TeeTimeList 1


Create or alter procedure UpdateTeeTime (@StandingID int null)
as
	Declare @ReturnCode INT
	Set @ReturnCode = 1
	IF @StandingID IS NULL
	RAISERROR('StandingID - Required Parameter: @Parameter',16,1)
	Else
	Begin
	
SELECT RequestedDate, RequestedTeeTime,StandingTeeTime.StandingID
FROM BookingTeeTime
INNER JOIN StandingTeeTime ON BookingTeeTime.StandingID = StandingTeeTime.StandingID
WHERE MemberID = @MemberID
AND CONCAT(RequestedDate, ' ', RequestedTeeTime) >= CONCAT(CAST(GETDATE() AS DATE), ' ', CAST(GETDATE() AS TIME))
IF @@ERROR = 0
Set @ReturnCode = 0
Else
	Raiserror('TeeTimeList - List Error: BookingTeeTime Table',16,1)
	End
Return @ReturnCode


CREATE OR ALTER PROCEDURE UpdateTeeTime (
    @StandingID INT,
    @MemberNumber1 VARCHAR(100) NULL,
    @Name1 VARCHAR(255) NULL,
    @MemberNumber2 VARCHAR(100) NULL,
    @Name2 VARCHAR(255) NULL,
    @MemberNumber3 VARCHAR(100) NULL,
    @Name3 VARCHAR(255) NULL,
    @MemberNumber4 VARCHAR(100) NULL,
    @Name4 VARCHAR(255) NULL,
    @RequestedTeeTime TIME,
    @RequestedDate DATE
)
AS
BEGIN
    DECLARE @ReturnCode INT;
    SET @ReturnCode = 1;

    IF @StandingID IS NULL
        RAISERROR('StandingID - Required Parameter: @StandingID is missing.', 16, 1);
    ELSE IF @MemberNumber1 IS NULL
        RAISERROR('MemberNumber1 - Required Parameter: @MemberNumber1 is missing.', 16, 1);
    ELSE IF @Name1 IS NULL
        RAISERROR('Name1 - Required Parameter: @Name1 is missing.', 16, 1);
    ELSE IF @RequestedTeeTime IS NULL
        RAISERROR('RequestedTeeTime - Required Parameter: @RequestedTeeTime is missing.', 16, 1);
    ELSE IF @RequestedDate IS NULL
        RAISERROR('RequestedDate - Required Parameter: @RequestedDate is missing.', 16, 1);
    ELSE BEGIN
        -- Check if the requested tee time is available
        IF EXISTS (
            SELECT 1
            FROM StandingTeeTime
            WHERE StandingID = @StandingID
        )
        BEGIN
            -- Update existing tee time request
            UPDATE StandingTeeTime
            SET MemberNumber1 = @MemberNumber1,
                Name1 = @Name1,
                MemberNumber2 = @MemberNumber2,
                Name2 = @Name2,
                MemberNumber3 = @MemberNumber3,
                Name3 = @Name3,
                MemberNumber4 = @MemberNumber4,
                Name4 = @Name4,
                RequestedTeeTime = @RequestedTeeTime,
                RequestedDate = @RequestedDate
            WHERE StandingID = @StandingID;

            SET @ReturnCode = 0;
        END
        ELSE BEGIN
            -- The requested tee time does not exist
            RAISERROR('Requested tee time does not exist.', 16, 1);
        END
    END

    RETURN @ReturnCode;
END;

select * from StandingTeeTime

exec UpdateTeeTime '1', '1010','Oliver','2','john',null,null,null,null,'10:11:00','2025-02-15'

CREATE PROCEDURE GetTeeTimeDetails
    @StandingID INT
AS
BEGIN
    SELECT * FROM StandingTeeTime WHERE StandingID = @StandingID;
END

exec GetTeeTimeDetails 1

--not done
Create table StaffCLB (
StaffID INT PRIMARY KEY IDENTITY
FirstName varchar (100)
LastName varchar (100)

)

select * from ClubMember