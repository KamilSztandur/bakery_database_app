/*
 * Kamil Sztandur 07.04.2022
 * Use this script to recreate useful views for this database.
 */

USE bakeryapp
GO

IF OBJECT_ID('main.[Sold products]') IS NOT NULL
    DROP VIEW main.[Sold products]
IF OBJECT_ID('main.[Total earnings per product]') IS NOT NULL
    DROP VIEW main.[Total earnings per product]
IF OBJECT_ID('main.[Bakeries earnings]') IS NOT NULL
    DROP VIEW main.[Bakeries earnings]
IF OBJECT_ID('main.[Premium clients showcase]') IS NOT NULL
    DROP VIEW main.[Premium clients showcase]
IF OBJECT_ID('main.GET_CLIENTS_TOTAL_MONEY_SPENT') IS NOT NULL
    DROP FUNCTION main.GET_CLIENTS_TOTAL_MONEY_SPENT
IF OBJECT_ID('main.[Clients expenses]') IS NOT NULL
    DROP VIEW main.[Clients expenses]
GO

-- Sold products
CREATE VIEW main.[Sold products] AS
    SELECT  r.id as 'Receipt ID',
            bakerycode AS 'Bakery code',
            (c.Surname + ' ' + c.Name) AS 'Customer',
            p.Name, totalprice AS 'Total price',
            date AS 'Date'
    FROM main.Receipts AS r, main.Products AS p, main.Clients as c
    WHERE p.Id = r.ProductId AND c.Id = r.ClientId;
GO

-- Earnings per product
CREATE VIEW main.[Total earnings per product] AS
    SELECT  p.Id,
            p.Name,
            p.Price as 'Price per unit',
            COUNT(r.TotalPrice) AS 'Units sold',
            ISNULL(SUM(r.TotalPrice), 0) AS 'Total earnings'
    FROM main.Products AS p
    LEFT JOIN main.Receipts AS r ON p.Id = r.ProductId
    GROUP BY p.Id, p.Name, p.Price;
GO

-- Earnings per bakery
CREATE VIEW main.[Bakeries earnings] AS
    SELECT  b.BakeryCode AS 'Bakery Code',
            (b.PostalCode + ' ' + b.TownName) AS 'Postal address',
            (b.StreetName + ' ' + CAST(b.StreetNumber AS NVARCHAR(10))) AS 'Street address',
            COUNT(r.TotalPrice) AS 'Units sold',
            ISNULL(SUM(r.TotalPrice), 0) AS 'Total earnings'
    FROM main.BAKERIES as b
    LEFT JOIN main.Receipts AS r ON b.BakeryCode = r.BakeryCode
    GROUP BY b.BakeryCode,
             (b.PostalCode + ' ' + b.TownName),
             (b.StreetName + ' ' + CAST(b.StreetNumber AS NVARCHAR(10)));
GO

-- Premium clients showcase
CREATE FUNCTION main.GET_CLIENTS_TOTAL_MONEY_SPENT(@clientId int)
RETURNS MONEY AS
BEGIN
    DECLARE @totalMoneySpent MONEY
    SET @totalMoneySpent = (SELECT SUM(TotalPrice) FROM main.Receipts WHERE ClientId = @clientId)

    RETURN @totalMoneySpent
END
GO

CREATE VIEW main.[Premium clients showcase] AS
    SELECT  c.id AS 'Client Id',
            (c.name + ' ' + c.surname) AS 'Client',
            CAST(
                IIF(
                    main.GET_CLIENTS_TOTAL_MONEY_SPENT(c.id) > d.MoneyThreshold AND c.Id != 1,
                    1,
                    0
                ) AS BIT
            ) AS 'Is premium'
    FROM main.Clients AS c, main.Discounts AS d
    WHERE d.Description = 'Premium client';
GO

-- Expenses per clients
CREATE VIEW main.[Clients expenses] AS
    SELECT  c.id AS 'Client Id',
            (c.name + ' ' + c.surname) AS 'Client',
            main.GET_Clients_TOTAL_MONEY_SPENT(c.id) AS 'Total expenses',
            COUNT(r.Id) AS 'Total units bought'
    FROM main.Clients AS c, main.Receipts AS r
    WHERE r.ClientId = c.Id
    GROUP BY c.id,
             (c.name + ' ' + c.surname),
             main.GET_Clients_TOTAL_MONEY_SPENT(c.id);
GO