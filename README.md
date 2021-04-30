
# Xpenser
Xpenser is a open source budget and expense tracking tool developed in Blazor Server.

This is a sample application for live coding sessions done on Twitch, you can also understand the code, watch the development and contribute by joining me https://www.twitch.tv/techierathore. 

## Application Features (To Be Developed)
 - Account Management 
 - Income Expense Management 
 - Month/Year wise statistics of expenses graphically 
 - Category wise statistics of expenses graphically 
 
 Stream Recordingsd available at [YouTube](https://www.youtube.com/playlist?list=PLhW718RDpMv6VHtHbOMbfhlSxdhD9VK9d) 
 
 
 ##### Code Execution prerequisites
 1. Visual Studio 2019 Any Edition 
 2. .NET 5 SDK 
 3. Maria DB Server 
 4. Database Management tool like TablePlus

##### Database Setup Steps 
 1. All the DML scripts are in 'Xpenser.DbMigration/Scripts' Folder
 2. Open Database Management tool of your choice and connect to MariaDb Database (We Suggest TablePlus)
 3. Execute All the Scripts of Scripts folder in the order mentioned in File Names. 

##### Code Building Steps 
 1. To build the code, open Xpenser.sln file in Visual Studio
 2.  Right Click on the Solution File in Solution Explorer and Select 'Restore Nuget Packages'.
 3.  Once Packages are restored, repeat previous step but this time select 'Rebuild Solution'
 4.  Once Build is sccussful click F5 to execute the application.


 

