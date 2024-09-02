# Ensek Technical Test

## Overview
This project requires setting up a local SQL Server database to support the solution. Follow the instructions below to get the database and the application configured.

## Setup Instructions

### 1. Execute the SQL Script
1. Navigate to the following directory in your project:
   ```
   Ensek_Techinical_Test\Ensek_Techinical_Test\SQL\
   ```
2. Locate the script file named `Tables and Data.sql`.
3. Execute this script on your local SQL Server instance. This will create the necessary database and tables required for the solution.

### 2. Configure the Application
1. Open the `appsettings.json` file located in the project’s root directory.
2. Modify the `ConnectionStrings` section to include the connection string for the newly created database. It should look something like this:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YourServerName;Database=YourDatabaseName;User Id=YourUsername;Password=YourPassword;"
   }
   ```
   - Replace `YourServerName`, `YourDatabaseName`, `YourUsername`, and `YourPassword` with your actual SQL Server instance details and credentials.

### 3. Run the Application
Once the above steps are completed, the application should be ready to run. You can build and launch the solution through Visual Studio or your preferred development environment.

## Notes
- Ensure that your SQL Server instance is running and accessible before executing the script.
- If you encounter any issues during setup, please verify that the SQL Server instance name and credentials are correctly configured in the `appsettings.json` file.

## Support
For any issues or questions, please contact the project maintainer.

---
