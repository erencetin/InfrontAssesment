# InfrontAssesment

The application basically retrieves stock data from api. And allows users to add it in their portfolio. 
As the first step you have to validate stock by pressing "Find" button. It retrieves current market price of the stock. 
Then you can add the stock in your portfolio. You can change buy price and contract count.
You can also close the position you have already opened. It removes relevant symbol from db. 

The Solution consists of 1 Console, 2 Class libraries and a test project. It uses SqlLite db to store portfolio data.


**InfrontAssesment.Core**

Core layer consists model classes and all interfaces implemented by the repositories and services. 
There is no business logic exists here. And it does not have any reference to the other projects.

**InfrontAssesment.Infrastructure**

This layer includes the logic of retrieving data from api and sqlite db.
And also has the business logic of adding stocks in adition to the persisted stock. And  the logic of closing position as well. 

**InfrontAssesment.WebUI**

As user interface i just used default Blazor template in VS. Because of the issues with SqlLite database i was supposed to use Blazor server side instead of web assembly.
Dependency injection has been configured in Program.cs in ui project.

**InfrontAssesment.Tests**

I wrote all unit tests on NUnit framework and for mocking i preferred Moq package. 
It covers the entire logic in stock operation service.


**Running the application**

Please set InfrontAssesment.WebUI as startup project and run it. Then please navigate into the Portfolio page.

**Prerequisites**

Visual Studio 2022
.NET6.0
