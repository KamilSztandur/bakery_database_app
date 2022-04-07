/*
 * Kamil Sztandur 07.04.2022
 * Use this script to recreate useful views for this database.
 */

USE bakeryapp
GO

IF OBJECT_ID('main.SoldProducts') IS NOT NULL
    DROP VIEW main.SoldProducts
IF OBJECT_ID('main.EarningsPerProduct') IS NOT NULL
    DROP VIEW main.EarningsPerProduct
IF OBJECT_ID('main.BakeriesEarnings') IS NOT NULL
    DROP VIEW main.BakeriesEarnings
IF OBJECT_ID('main.PremiumClients') IS NOT NULL
    DROP VIEW main.PremiumClients
IF OBJECT_ID('main.GET_CLIENTS_TOTAL_MONEY_SPENT') IS NOT NULL
    DROP FUNCTION main.GET_CLIENTS_TOTAL_MONEY_SPENT
IF OBJECT_ID('main.ClientsExpenses') IS NOT NULL
    DROP VIEW main.ClientsExpenses
GO

-- Sold products
CREATE VIEW main.SoldProducts AS
    SELECT  r.id as 'ReceiptID',
            bakerycode AS 'BakeryCode',
            (c.Surname + ' ' + c.Name) AS 'Customer',
            p.Name, totalprice AS 'TotalPrice',
            date AS 'Date'
    FROM main.Receipts AS r, main.Products AS p, main.Clients as c
    WHERE p.Id = r.ProductId AND c.Id = r.ClientId;
GO

-- Earnings per product
CREATE VIEW main.EarningsPerProduct AS
    SELECT  p.Id,
            p.Name,
            p.Price as 'PricePerUnit',
            COUNT(r.TotalPrice) AS 'UnitsSold',
            ISNULL(SUM(r.TotalPrice), 0) AS 'TotalEarnings'
    FROM main.Products AS p
    LEFT JOIN main.Receipts AS r ON p.Id = r.ProductId
    GROUP BY p.Id, p.Name, p.Price;
GO

-- Earnings per bakery
CREATE VIEW main.BakeriesEarnings AS
    SELECT  b.BakeryCode AS 'BakeryCode' ,
            (b.PostalCode + ' ' + b.TownName) AS 'PostalAddress',
            (b.StreetName + ' ' + CAST(b.StreetNumber AS NVARCHAR(10))) AS 'StreetAddress',
            COUNT(r.TotalPrice) AS 'UnitsSold',
            ISNULL(SUM(r.TotalPrice), 0) AS 'TotalEarnings'
    FROM main.BAKERIES as b
    LEFT JOIN main.Receipts AS r ON b.BakeryCode = r.BakeryCode
    GROUP BY b.BakeryCode,
             (b.PostalCode + ' ' + b.TownName),
             (b.StreetName + ' ' + CAST(b.StreetNumber AS NVARCHAR(10)));
GO

-- Premium clients showcase
CREATE FUNCTION main.GET_CLIENTS_TOTAL_MONEY_SPENT(@clientId int)
RETURNS FLOAT AS
BEGIN
    DECLARE @totalMoneySpent FLOAT
    SET @totalMoneySpent = (SELECT SUM(TotalPrice) FROM main.Receipts WHERE ClientId = @clientId)

    RETURN @totalMoneySpent
END
GO

CREATE VIEW main.PremiumClients AS
    SELECT  c.id AS 'ClientID',
            (c.name + ' ' + c.surname) AS 'Client',
            CAST(
                IIF(
                    main.GET_CLIENTS_TOTAL_MONEY_SPENT(c.id) > d.MoneyThreshold AND c.Id != 1,
                    1,
                    0
                ) AS BIT
            ) AS 'IsPremium'
    FROM main.Clients AS c, main.Discounts AS d
    WHERE d.Description = 'Premium client';
GO

-- Expenses per clients
CREATE VIEW main.ClientsExpenses AS
    SELECT  c.id AS 'ClientID',
            (c.name + ' ' + c.surname) AS 'Client',
            main.GET_Clients_TOTAL_MONEY_SPENT(c.id) AS 'TotalExpenses',
            COUNT(r.Id) AS 'TotalUnitsBought'
    FROM main.Clients AS c, main.Receipts AS r
    WHERE r.ClientId = c.Id
    GROUP BY c.id,
             (c.name + ' ' + c.surname),
             main.GET_Clients_TOTAL_MONEY_SPENT(c.id);
GO