USE master
GO

/*
DROP TABLE bakery.BAKERIES
DROP TABLE bakery.RECEIPTS
DROP TABLE bakery.CLIENTS
DROP TABLE bakery.PRODUCTS
DROP TABLE bakery.DISCOUNTS
*/

IF OBJECT_ID('bakery.BAKERIES') IS NULL
BEGIN
	CREATE TABLE bakery.BAKERIES
	(
	    BakeryCode		NVARCHAR(2)     NOT NULL CONSTRAINT PK_BAKERIES PRIMARY KEY
	,	TownName	    NVARCHAR(100)   NOT NULL
	,	StreetName	    NVARCHAR(100)   NOT NULL
	,	StreetNumber	INT             NOT NULL
	,   PostalCode      NVARCHAR(6)     NOT NULL
	)
END
GO

IF OBJECT_ID('bakery.RECEIPTS') IS NULL
BEGIN
	CREATE TABLE bakery.RECEIPTS
	(
	    Id		        INT             NOT NULL    IDENTITY            CONSTRAINT PK_RECEIPTS PRIMARY KEY
	,	ClientId	    INT             NOT NULL    DEFAULT -1
	,	ProductId	    INT             NOT NULL
	,   BakeryCode      NVARCHAR(2)     NOT NULL
	,	Value	        MONEY           NOT NULL    DEFAULT 0
	,   Date            DATETIME        NOT NULL    DEFAULT GETDATE()
	)
END
GO

IF OBJECT_ID('bakery.CLIENTS') IS NULL
BEGIN
	CREATE TABLE bakery.CLIENTS
	(
	    Id		        INT             NOT NULL    IDENTITY    CONSTRAINT PK_CLIENTS PRIMARY KEY
	,	Name	        NVARCHAR(32)    NOT NULL
	,	Surname	        NVARCHAR(32)    NOT NULL
	,	IsPremium	    BIT             NOT NULL    DEFAULT 0
	)
END
GO

IF OBJECT_ID('bakery.PRODUCTS') IS NULL
BEGIN
	CREATE TABLE bakery.PRODUCTS
	(
	    Id		        INT             NOT NULL    IDENTITY    CONSTRAINT PK_PRODUCTS PRIMARY KEY
	,	Name	        NVARCHAR(32)    NOT NULL
	,	Price	        MONEY           NOT NULL
	,	IsPremium	    BIT             NOT NULL    DEFAULT 0
	)
END
GO

IF OBJECT_ID('bakery.DISCOUNTS') IS NULL
BEGIN
	CREATE TABLE bakery.DISCOUNTS
	(
	    Id		        INT             NOT NULL    IDENTITY    CONSTRAINT PK_DISCOUNTS PRIMARY KEY
	,	MoneyThreshold	MONEY           NOT NULL
	,	ValueInPercents DECIMAL(2, 2)   NOT NULL    DEFAULT 0
	,	IsActive	    BIT             NOT NULL    DEFAULT 0
	)
END
GO