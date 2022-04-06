/*
 * Kamil Sztandur 06.04.2022
 * Use this script to recreate useful views for this database.
 */

USE master
GO

IF OBJECT_ID('bakery.[Sold products]') IS NOT NULL
    DROP VIEW bakery.[Sold products]
IF OBJECT_ID('bakery.[Total earnings per product]') IS NOT NULL
    DROP VIEW bakery.[Total earnings per product]
IF OBJECT_ID('bakery.[Bakeries earnings]') IS NOT NULL
    DROP VIEW bakery.[Bakeries earnings]
IF OBJECT_ID('bakery.[Premium clients showcase]') IS NOT NULL
    DROP VIEW bakery.[Premium clients showcase]
IF OBJECT_ID('bakery.GET_CLIENTS_TOTAL_MONEY_SPENT') IS NOT NULL
    DROP FUNCTION bakery.GET_CLIENTS_TOTAL_MONEY_SPENT
IF OBJECT_ID('bakery.[Clients expenses]') IS NOT NULL
    DROP VIEW bakery.[Clients expenses]
GO

-- Sold products
CREATE VIEW bakery.[Sold products] AS
    SELECT  r.id as 'Receipt ID',
            bakerycode AS 'Bakery code',
            (c.Surname + ' ' + c.Name) AS 'Customer',
            p.Name, totalprice AS 'Total price',
            date AS 'Date'
    FROM bakery.RECEIPTS AS r, bakery.PRODUCTS AS p, bakery.CLIENTS as c
    WHERE p.Id = r.ProductId AND c.Id = r.ClientId;
GO

-- Earnings per product
CREATE VIEW bakery.[Total earnings per product] AS
    SELECT  p.Id,
            p.Name,
            p.Price as 'Price per unit',
            COUNT(r.TotalPrice) AS 'Units sold',
            ISNULL(SUM(r.TotalPrice), 0) AS 'Total earnings'
    FROM bakery.PRODUCTS AS p
    LEFT JOIN bakery.RECEIPTS AS r ON p.Id = r.ProductId
    GROUP BY p.Id, p.Name, p.Price;
GO

-- Earnings per bakery
CREATE VIEW bakery.[Bakeries earnings] AS
    SELECT  b.BakeryCode AS 'Bakery Code',
            (b.PostalCode + ' ' + b.TownName) AS 'Postal address',
            (b.StreetName + ' ' + CAST(b.StreetNumber AS NVARCHAR(10))) AS 'Street address',
            COUNT(r.TotalPrice) AS 'Units sold',
            ISNULL(SUM(r.TotalPrice), 0) AS 'Total earnings'
    FROM bakery.BAKERIES as b
    LEFT JOIN bakery.RECEIPTS AS r ON b.BakeryCode = r.BakeryCode
    GROUP BY b.BakeryCode,
             (b.PostalCode + ' ' + b.TownName),
             (b.StreetName + ' ' + CAST(b.StreetNumber AS NVARCHAR(10)));
GO

-- Premium clients showcase
CREATE FUNCTION bakery.GET_CLIENTS_TOTAL_MONEY_SPENT(@clientId int)
RETURNS MONEY AS
BEGIN
    DECLARE @totalMoneySpent MONEY
    SET @totalMoneySpent = (SELECT SUM(TotalPrice) FROM bakery.RECEIPTS WHERE ClientId = @clientId)

    RETURN @totalMoneySpent
END
GO

CREATE VIEW bakery.[Premium clients showcase] AS
    SELECT  c.id AS 'Client Id',
            (c.name + ' ' + c.surname) AS 'Client',
            CAST(
                IIF(
                    bakery.GET_CLIENTS_TOTAL_MONEY_SPENT(c.id) > d.MoneyThreshold AND c.Id != 1,
                    1,
                    0
                ) AS BIT
            ) AS 'Is premium'
    FROM bakery.CLIENTS AS c, bakery.DISCOUNTS AS d
    WHERE d.Description = 'Premium client';
GO

-- Expenses per clients
CREATE VIEW bakery.[Clients expenses] AS
    SELECT  c.id AS 'Client Id',
            (c.name + ' ' + c.surname) AS 'Client',
            bakery.GET_CLIENTS_TOTAL_MONEY_SPENT(c.id) AS 'Total expenses',
            COUNT(r.Id) AS 'Total units bought'
    FROM bakery.CLIENTS AS c, bakery.RECEIPTS AS r
    WHERE r.ClientId = c.Id
    GROUP BY c.id,
             (c.name + ' ' + c.surname),
             bakery.GET_CLIENTS_TOTAL_MONEY_SPENT(c.id);
GO