The main idea of this project is to store, update and retrieve different types of beer abd their associated breweries and bars.

Overall this project contains 18 endpoints to serve the above mentioned functionality.

Follow the steps below to run the application.

-> Navigate to https://github.com/priyanandavaram/BeerManagementCoreSolution and clone the repository on your local machine.

-> Once the repository is cloned, Please restore the SQL Server database back up from the folder DatabaseBackup by following below steps.

-> Connect to SSMS -> Select Databases folder within your DB server -> Right click ->Restore Databases.

-> Once the database is successfully restored, Open the .net project in visual studio and modify the connection string in appsettings.development.json.

-> Set BeerManagement.Web as the startup project and run the application and you'll displayed with swagger page.

-> Test the endpoints using swagger. 


Attached is the sample data for performing POST & PUT operations to test the endpoints.

POST /beer 

	{
		"beerId": 0,
		"beerName": "Corona Premier ",
		"percentageAlcoholByVolume": 4.0
	}

PUT /beer/{id}

	{
	  "beerId": 9,
	  "beerName": "Coors Light Beer",
	  "percentageAlcoholByVolume": 8.9
	}

POST /bar

	{
	  "barId": 0,
	  "barName": "Cosmic Cocktails",
	  "barAddress": "Cardiff, Wales"
	}

PUT /bar/{id}

	{
	  "barId": 2,
	  "barName": "Liquor Lighthouse",
	  "barAddress": "Cardiff Main Street"
	}

POST /brewery 

	{
	  "breweryId": 0,
	  "breweryName": "The Brewdog"
	}

PUT /brewery/{id}

	{
		"breweryId": 5,
		"breweryName": "Dageraad"
	}

POST /brewery/beer

	{
	  "breweryId": 3,
	  "beerId": 5
	}

POST /bar/beer

{
	  "barId": 3,
	  "beerId": 5
	}

	