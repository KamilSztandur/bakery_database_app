# Zbigniew Habadzibadło's bakery management database app.

## Table of contents
* [About](#about)
* [Technologies](#technologies)
* [Setup](#setup)
* [Functionalities](#functionalities)
* [Showcase](#showcase)
* [Notes](#notes)

## About
Database app for Zbigniew Habadzibadło's bakery management.
	
## Technologies
* .NET 6.0
* ASP.NET MVC 6.0
* Microsoft Entity Framework
* MS SQL Express (localDB)
* SQL
* Onion Architecture
* Domain-Driven Design

## Setup
1. Install all required .NET versions.
2. Install MS SQL Express on your machine.
3. Initialize bakeryapp database and tables by using:
``
initialize_tables.sql
``
4. Initialize bakeryapp views by using:
``
initialize_views.sql
``


5. Enter Powershell and go to BakeryManagement.Infrastructure, execute these two commands:


``
dotnet ef --startup-project ../BakeryManager.WebAPI/BakeryManager.WebAPI.csproj migrations add setup
``


``
dotnet ef --startup-project ../BakeryManager.WebAPI/BakeryManager.WebAPI.csproj database update
``


6. Launch WebAPI (IIS EXpress)
7. Launch WebApp.
8. Have fun!

## Functionalities
* Intuitivie GUI for database management.
* Support and distinguish between bakeries without any conflicts using the Bakery Code.
* Bakery Code consists of two characters and is unique due to being the PK.
* The app allows user to generate sales reports.
* The app allows user to generate a sales by the types of products in the "Reports" tab.
* Tha app allows user to save all reports to their local machine by exporting it to CSV file.
* The system does not allow user to enter incorrect data. Input verification takes place already at the level of web application in the forms to which the data is entered, then in the web API controllers, then in the infrastructure layer services (e.g. syntax), then in the infrastructure layer repositories (data compatibility with tha database) and in the MS SQL Express environment.
* System does not allow user to list a sale for a non-existent bakery. At level of repositories connecting the system with the ORM Entity Framework, it's verified whether a given bakery exists in database.
* The system supports so-called regular customers, stores their data and automatically calculates discounts for them as part of loyalty programs (GetTotalPrice function in SQL).
* The system assigns customers and ID number and stores their first and last name. The amount of money spent is dynamically calculated per call and is based on receipts data table.
* The is a "Premium Client" discount in the system, which entitles its regular customers to 15% discount if they spent a total of PLN 1000. Additionally, there is a report that distinguishes premium customers from all customers.
* At the time of transaction, the premium customer discount is automatically applied by the system without the need of the participation of real living persons. Then the price of each purchase is reduced by 15% for premium client.

## Showcase
Home Page:
<br/>

![image](https://user-images.githubusercontent.com/61657553/163723379-61bdcaf0-bfdd-4c50-a247-37b3193074d0.png)

Sample table view:
<br/>
![image](https://user-images.githubusercontent.com/61657553/163723927-5536718a-0dbe-4f54-8745-7feaa192bbeb.png)
<br/>

Create, Edit and Delete all records:
<br/>
![image](https://user-images.githubusercontent.com/61657553/163723424-82c2ab52-0a62-4413-9025-6e1ddd18eb6a.png)
<br/>
![image](https://user-images.githubusercontent.com/61657553/163723431-34137343-2375-4521-b364-5713909f9ef9.png)
<br/>
![image](https://user-images.githubusercontent.com/61657553/163723436-061b9801-8a01-4aa6-bf51-d67e869c1a44.png)
<br/>

Use "Reports" tab to analyze your sales and bakery data:
![image](https://user-images.githubusercontent.com/61657553/163723463-9bb7dc6a-bc9a-43f6-929c-359b7ffb6ec1.png)
<br/>

Export your reports to CSV by clicking "Export to CSV" button and save it on your local machine:
<br/>
![image](https://user-images.githubusercontent.com/61657553/163723994-71dd1812-86c7-48df-af22-c156c29f43eb.png)
<br/>

## Notes
* My one-man university project.
* Name refers to one of the videos of GF Darwin, team of polish creators of amateur cinema on polish YouTube. [Click here to watch the video.](https://youtu.be/R5b4VUkcn8c)
