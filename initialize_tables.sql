/*
 * Kamil Sztandur 06.04.2022
 * Use this script to reset database to default values.
 */

USE master
GO


-- Total reset

IF OBJECT_ID('bakery.RECEIPTS') IS NOT NULL
    DROP TABLE bakery.RECEIPTS
IF OBJECT_ID('bakery.CLIENTS') IS NOT NULL
    DROP TABLE bakery.CLIENTS
IF OBJECT_ID('bakery.PRODUCTS') IS NOT NULL
    DROP TABLE bakery.PRODUCTS
IF OBJECT_ID('bakery.DISCOUNTS') IS NOT NULL
    DROP TABLE bakery.DISCOUNTS
IF OBJECT_ID('bakery.GET_TOTAL_RECEIPT_PRICE') IS NOT NULL
    drop FUNCTION bakery.GET_TOTAL_RECEIPT_PRICE
IF OBJECT_ID('bakery.BAKERIES') IS NOT NULL
    DROP TABLE bakery.BAKERIES

-- BAKERIES
IF OBJECT_ID('bakery.BAKERIES') IS NULL
BEGIN
	CREATE TABLE bakery.BAKERIES
	(
	    BakeryCode		NVARCHAR(2)     NOT NULL    PRIMARY KEY
	,	TownName	    NVARCHAR(100)   NOT NULL
	,	StreetName	    NVARCHAR(100)   NOT NULL
	,	StreetNumber	INT             NOT NULL
	,   PostalCode      NVARCHAR(6)     NOT NULL
	)

	INSERT INTO bakery.BAKERIES (BakeryCode, TownName, StreetName, StreetNumber, PostalCode)
    VALUES ('AB', 'Warsaw', 'Jerozolimska', 69, '01-934')

	INSERT INTO bakery.BAKERIES (BakeryCode, TownName, StreetName, StreetNumber, PostalCode)
    VALUES ('CD', 'Warsaw', 'Jaszowiecka', 45, '01-352')

	INSERT INTO bakery.BAKERIES (BakeryCode, TownName, StreetName, StreetNumber, PostalCode)
    VALUES ('EF', 'Warsaw', 'Szeroka', 20, '01-420')
END
GO


-- DISCOUNTS
IF OBJECT_ID('bakery.DISCOUNTS') IS NULL
BEGIN
	CREATE TABLE bakery.DISCOUNTS
	(
	    Id		        INT             NOT NULL    IDENTITY PRIMARY KEY
	,	MoneyThreshold	MONEY           NOT NULL
	,	ValueInPercents DECIMAL(2, 2)   NOT NULL    DEFAULT 0
	,	IsActive	    BIT             NOT NULL    DEFAULT 0
	,   Description     nvarchar(64)    NULL
	)

	INSERT INTO bakery.DISCOUNTS (MoneyThreshold, ValueInPercents, IsActive, Description)
	VALUES (1000, 0.15, 1, 'Premium client')
END
GO


-- CLIENTS
IF OBJECT_ID('bakery.CLIENTS') IS NULL
BEGIN
	CREATE TABLE bakery.CLIENTS
	(
	    Id		        INT             NOT NULL    IDENTITY PRIMARY KEY
	,	Name	        NVARCHAR(32)    NOT NULL
	,	Surname	        NVARCHAR(32)    NOT NULL
	)

	INSERT INTO bakery.CLIENTS (Name, Surname)
	VALUES ('Anonymous', 'Anonymous')

	INSERT INTO bakery.CLIENTS (Name, Surname)
	VALUES ('John', 'Smith')

	INSERT INTO bakery.CLIENTS (Name, Surname)
	VALUES ('Elizabeth', 'Apple')

	INSERT INTO bakery.CLIENTS (Name, Surname)
	VALUES ('Chris', 'Chan')


END
GO


-- PRODUCTS
IF OBJECT_ID('bakery.PRODUCTS') IS NULL
BEGIN
	CREATE TABLE bakery.PRODUCTS
	(
	    Id		        INT             NOT NULL    IDENTITY PRIMARY KEY
	,	Name	        NVARCHAR(32)    NOT NULL
	,	Price	        MONEY           NOT NULL
	)

	INSERT INTO bakery.PRODUCTS (Name, Price)
	VALUES ('Plain Bread', 1.00)

	INSERT INTO bakery.PRODUCTS (Name, Price)
	VALUES ('Rye Bread', 1.25)

	INSERT INTO bakery.PRODUCTS (Name, Price)
	VALUES ('Kaiser roll', 0.50)

	INSERT INTO bakery.PRODUCTS (Name, Price)
	VALUES ('Sweet roll', 2.50)

	INSERT INTO bakery.PRODUCTS (Name, Price)
	VALUES ('Krakow bagpipe', 4.30)
END
GO


-- RECEIPTS
CREATE FUNCTION bakery.GET_TOTAL_RECEIPT_PRICE(@clientId int, @productId int)
RETURNS MONEY AS
BEGIN
    DECLARE @totalPrice MONEY

    -- SET NORMAL PRICE AS TOTAL PRICE
    SET @totalPrice = (SELECT Price FROM bakery.PRODUCTS WHERE Id = @productId)

    -- CALCULATE TOTAL PRICE REGARDING ALL POSSIBLE DISCOUNTS
    DECLARE @totalMoneySpent MONEY
    SET @totalMoneySpent = (SELECT SUM(TotalPrice) FROM bakery.RECEIPTS WHERE ClientId = @clientId)

    DECLARE @discountId int

    DECLARE DISCOUNTS_CURSOR CURSOR LOCAL STATIC READ_ONLY FORWARD_ONLY
    FOR SELECT DISTINCT Id FROM bakery.DISCOUNTS

    OPEN DISCOUNTS_CURSOR
    FETCH NEXT FROM DISCOUNTS_CURSOR INTO @discountId
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- CALCULATE CURRENT DISCOUNT
        DECLARE @discountThreshold MONEY
        DECLARE @discountMultiplier DECIMAL(2, 2)

        SET @discountThreshold = (SELECT MoneyThreshold FROM bakery.DISCOUNTS WHERE Id = @discountId)

        IF @totalMoneySpent >= @discountThreshold
        BEGIN
            SET @discountMultiplier = 1 - (SELECT ValueInPercents FROM bakery.DISCOUNTS WHERE Id = @discountId)
            SET @totalPrice = @totalPrice * @discountMultiplier
        END

        FETCH NEXT FROM DISCOUNTS_CURSOR INTO @discountId
    END
    CLOSE DISCOUNTS_CURSOR
    DEALLOCATE DISCOUNTS_CURSOR

    RETURN @totalPrice
END
GO

IF OBJECT_ID('bakery.RECEIPTS') IS NULL
BEGIN
	CREATE TABLE bakery.RECEIPTS
	(
	    Id		        INT             NOT NULL    IDENTITY            PRIMARY KEY
	,	ClientId	    INT             NOT NULL    DEFAULT -1          FOREIGN KEY REFERENCES bakery.CLIENTS(Id)
	,	ProductId	    INT             NOT NULL                        FOREIGN KEY REFERENCES bakery.PRODUCTS(Id)
	,   BakeryCode      NVARCHAR(2)     NOT NULL                        FOREIGN KEY REFERENCES bakery.BAKERIES(BakeryCode)
	,	TotalPrice	    MONEY           NOT NULL    DEFAULT 0
	,   Date            DATETIME        NOT NULL    DEFAULT GETDATE()
	)

	INSERT INTO bakery.RECEIPTS (ClientId, ProductId, BakeryCode, TotalPrice)
	VALUES (2, 1, 'AB', bakery.GET_TOTAL_RECEIPT_PRICE(2, 1))

	INSERT INTO bakery.RECEIPTS (ClientId, ProductId, BakeryCode, TotalPrice)
	VALUES (2, 1, 'CD', bakery.GET_TOTAL_RECEIPT_PRICE(2, 1))

	INSERT INTO bakery.RECEIPTS (ClientId, ProductId, BakeryCode, TotalPrice)
	VALUES (3, 2, 'EF', bakery.GET_TOTAL_RECEIPT_PRICE(3, 2))

	INSERT INTO bakery.RECEIPTS (ClientId, ProductId, BakeryCode, TotalPrice)
	VALUES (4, 3, 'EF', bakery.GET_TOTAL_RECEIPT_PRICE(4, 3))

	INSERT INTO bakery.RECEIPTS (ClientId, ProductId, BakeryCode)
	VALUES (1, 1, 'AB')
END
GO