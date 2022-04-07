/*
 * Kamil Sztandur 07.04.2022
 * Use this script to initialize bakeryapp database
 */

USE bakeryapp;
GO;


IF OBJECT_ID('main.Bakeries') IS NOT NULL
    DROP TABLE main.Bakeries
IF OBJECT_ID('main.Clients') IS NOT NULL
    DROP TABLE main.Clients
IF OBJECT_ID('main.Discounts') IS NOT NULL
    DROP TABLE main.Discounts
IF OBJECT_ID('main.Products') IS NOT NULL
    DROP TABLE main.Products
IF OBJECT_ID('main.GET_TOTAL_RECEIPT_PRICE') IS NOT NULL
    drop FUNCTION main.GET_TOTAL_RECEIPT_PRICE
IF OBJECT_ID('main.ADD_NEW_RECEIPT') IS NOT NULL
    drop PROCEDURE main.ADD_NEW_RECEIPT
IF OBJECT_ID('main.Receipts') IS NOT NULL
    DROP TABLE main.Receipts
GO;

IF OBJECT_ID('main.Bakeries') IS NULL
BEGIN
    CREATE TABLE main.Bakeries
    (
        BakeryCode 		    nvarchar(450) 	NOT NULL,
        TownName 		    nvarchar(max) 	NOT NULL,
        StreetName 		    nvarchar(max) 	NOT NULL,
        StreetNumber 	    int 			NOT NULL,
        PostalCode 		    nvarchar(max) 	NOT NULL,
        CONSTRAINT 		    PK_Bakeries 	PRIMARY KEY (BakeryCode)
    );

    INSERT INTO main.Bakeries (BakeryCode, TownName, StreetName, StreetNumber, PostalCode)
    VALUES ('AB', 'Warsaw', 'Jerozolimska', 69, '01-934')

	INSERT INTO main.Bakeries (BakeryCode, TownName, StreetName, StreetNumber, PostalCode)
    VALUES ('CD', 'Warsaw', 'Jaszowiecka', 45, '01-352')

	INSERT INTO main.Bakeries (BakeryCode, TownName, StreetName, StreetNumber, PostalCode)
    VALUES ('EF', 'Warsaw', 'Szeroka', 20, '01-420')
END
GO;

IF OBJECT_ID('main.Clients') IS NULL
BEGIN
    CREATE TABLE main.Clients
    (
        Id 				    int 			NOT NULL 			IDENTITY,
        Name 			    nvarchar(max) 	NOT NULL,
        Surname 		    nvarchar(max) 	NOT NULL,
        CONSTRAINT 		    PK_Clients 		PRIMARY KEY (Id)
    );

	INSERT INTO main.Clients (Name, Surname)
	VALUES ('Anonymous', 'Anonymous')

	INSERT INTO main.Clients (Name, Surname)
	VALUES ('John', 'Smith')

	INSERT INTO main.Clients (Name, Surname)
	VALUES ('Elizabeth', 'Apple')

	INSERT INTO main.Clients (Name, Surname)
	VALUES ('Chris', 'Chan')
END
GO;

IF OBJECT_ID('main.Discounts') IS NULL
BEGIN
    CREATE TABLE main.Discounts
    (
        Id 					int 		    NOT NULL 			IDENTITY,
        MoneyThreshold 	    float 		    NOT NULL,
        ValueInPercents 	float 		    NOT NULL,
        IsActive 			bit 		    NOT NULL,
        Description 		nvarchar(max) 	NULL,
        CONSTRAINT 		    PK_Discounts 		PRIMARY KEY (Id)
    );

    INSERT INTO main.Discounts (MoneyThreshold, ValueInPercents, IsActive, Description)
	VALUES (1000, 0.15, 1, 'Premium client')
END
GO;

IF OBJECT_ID('main.Products') IS NULL
BEGIN
    CREATE TABLE main.Products
    (
        Id 					int 				NOT NULL 			IDENTITY,
        Name 				nvarchar(max) 	    NOT NULL,
        Price 				float 				NOT NULL,
        CONSTRAINT 		    PK_Products		    PRIMARY KEY (Id)
    );

	INSERT INTO main.Products (Name, Price)
	VALUES ('Plain Bread', 1.00)

	INSERT INTO main.Products (Name, Price)
	VALUES ('Rye Bread', 1.25)

	INSERT INTO main.Products (Name, Price)
	VALUES ('Kaiser roll', 0.50)

	INSERT INTO main.Products (Name, Price)
	VALUES ('Sweet roll', 2.50)

	INSERT INTO main.Products (Name, Price)
	VALUES ('Krakow bagpipe', 4.30)
END
GO;

CREATE FUNCTION main.GET_TOTAL_RECEIPT_PRICE(@clientId int, @productId int)
RETURNS MONEY AS
BEGIN
    DECLARE @totalPrice float

    -- SET NORMAL PRICE AS TOTAL PRICE
    SET @totalPrice = (SELECT Price FROM main.Products WHERE Id = @productId)

    -- Skip discounts for non registered clients
    IF @clientId = 1
        return @totalPrice

    -- CALCULATE TOTAL PRICE REGARDING ALL POSSIBLE DISCOUNTS
    DECLARE @totalMoneySpent float
    SET @totalMoneySpent = (SELECT SUM(TotalPrice) FROM main.Receipts WHERE ClientId = @clientId)

    DECLARE @discountId int

    DECLARE DISCOUNTS_CURSOR CURSOR LOCAL STATIC READ_ONLY FORWARD_ONLY
    FOR SELECT DISTINCT Id FROM main.Discounts

    OPEN DISCOUNTS_CURSOR
    FETCH NEXT FROM DISCOUNTS_CURSOR INTO @discountId
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- CALCULATE CURRENT DISCOUNT
        DECLARE @discountThreshold float
        DECLARE @discountMultiplier DECIMAL(2, 2)

        SET @discountThreshold = (SELECT MoneyThreshold FROM main.Discounts WHERE Id = @discountId)

        IF @totalMoneySpent >= @discountThreshold
        BEGIN
            SET @discountMultiplier = 1 - (SELECT ValueInPercents FROM main.Discounts WHERE Id = @discountId)
            SET @totalPrice = @totalPrice * @discountMultiplier
        END

        FETCH NEXT FROM DISCOUNTS_CURSOR INTO @discountId
    END
    CLOSE DISCOUNTS_CURSOR
    DEALLOCATE DISCOUNTS_CURSOR

    RETURN @totalPrice
END
GO;

CREATE PROCEDURE main.ADD_NEW_RECEIPT @clientId INT, @productId INT, @bakeryCode NVARCHAR(2)
AS
BEGIN
    DECLARE @totalPrice float
    SET @totalPrice = main.GET_TOTAL_RECEIPT_PRICE (@clientId, @productId)

    INSERT INTO main.Receipts(ClientId, ProductId, BakeryCode, TotalPrice, Date)
	VALUES (@clientId, @productId, @bakeryCode, @totalPrice, GETDATE())
END
GO;

IF OBJECT_ID('main.Receipts') IS NULL
BEGIN
    CREATE TABLE main.Receipts
    (
        Id 				    int 				NOT NULL 			IDENTITY,
        ClientId 		    int					NOT NULL,
        ProductId 		    int 				NOT NULL,
        BakeryCode 	        nvarchar(max) 		NOT NULL,
        TotalPrice 	        float 				NOT NULL,
        Date 			    datetime2 			NOT NULL,
        CONSTRAINT 	        PK_Receipts 		PRIMARY KEY (Id)
    )

	EXEC main.ADD_NEW_RECEIPT @clientId = 2, @productId = 1, @bakeryCode = 'AB';
	EXEC main.ADD_NEW_RECEIPT @clientId = 2, @productId = 1, @bakeryCode = 'CD';
	EXEC main.ADD_NEW_RECEIPT @clientId = 3, @productId = 2, @bakeryCode = 'EF';
	EXEC main.ADD_NEW_RECEIPT @clientId = 4, @productId = 3, @bakeryCode = 'EF';
	EXEC main.ADD_NEW_RECEIPT @clientId = 1, @productId = 1, @bakeryCode = 'AB';
END
GO;