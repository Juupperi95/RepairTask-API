# Steps to run API
## 1 Setup database
- API is configured to use MariaDb 10.4 and this repository has SQL-script to set up the database. For example DbSetup.sql-script can be run as a query through some GUI such as MySqlWorkbench.  
- If seeddata is used DbSetup.sql-script read-path needs to be changed to match.  
- Current setup expects to use connection parameters listed in appsettings.json file but they can be changed by editing the appsettings.json file.  
- Between API-startups it is not required to setup database up again.  
## 2 Install dependencies
Dependencies can be installed through Visual Studio NugetManager
- **Required dependencies for ApplicationCore-project:**  
-Microsoft.EntityFrameworkCore (tested with **3.1.1 version**)  
-Microsoft.EntityFrameworkCore.Analyzers (**3.1.1version** )  
-Microsoft.NETCore.App **(2.2.0 version)**  
-Pomelo.EntityFrameworkCore.MySql **(3.1.1 version)**  


-  **Required dependencies for Web-project:**
-Microsoft.AspNetCore.All **(2.2.8)**  
-Microsoft.NETCore.App **(2.2.0)**  
-Swashbuckle.AspNetCore.Swagger **(5.0.0)**  
-Swashbuckle.AspNetCore.SwaggerGen **(5.0.0)**  
-Swashbuckle.AspNetCore.SwaggerUI **(5.0.0)**  

## 3 Build and run project
- Project can be opened with ServiceManual.sln file.  
- To build and run you can use Visual Studio IDE (Press green "Run-button").  
## 4 Testing the endpoints
- Swagger UI should open instantly when project is run and it allows you to create HTTP-queries easily and test API through the UI.  

## 5 Quick notes
- Unit tests for services are not implemented due to using EntityFramework which doesnt offer an easy way to access to database outside of the main project.  
- Both criticality and completed-status are only implemented as integers at the moment.  
- Criticality can have values between 1-3. 3 being highest and 1 being lowest.  
- Completed-status is displayed as either 1 (task completed) or 0 (incompleted).  
