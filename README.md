# Introduction 

C# Repository Pattern CRUD on .NET core 6.


##### POC - Application to Review/Test products
We have used:
- .Net Core 6
- Azure SQL DB
- Azure Active Directory B2C ( to login use Microsoft )

The project consist of :

#####  WRP3.API  (API)
Temporary URL: https://wrp-api.azurewebsites.net
#####  WRP3.BackOffice  (Web UI)
- Temporary URL: https://wrp-backoffice.azurewebsites.net
- Template used from creative-tim.com (Free Template)

#####  WRP3.DataAccess
#####  WRP3.Domain
#####  WRP3.IServices
#####  WRP3.Infrastructure (External Service)
- Google Recaptca V2
- API Serivces

##### WRP3.Services
#####  WRP3.Web (FrontEnd-UI)  
- Temporary URL: https://wrp.azurewebsites.net
- ASP.net Core Template
- Google Recaptcah V2

# Database tables
![image](https://user-images.githubusercontent.com/20483242/187946049-74c3a062-35f1-41bd-81ee-50fb96172076.png)

# Azure Apps

![image](https://user-images.githubusercontent.com/20483242/187948216-91e7a9a5-7cea-46a2-b3ab-b6ad3b9bb6b9.png)


# How to run the App
Set the API as s startup app
###### Change the connection str to you localmachine
"ConnectionStr": "Server=(localdb)\\MSSQLLocalDB;Database=WRP3DB;Trusted_Connection=True;MultipleActiveResultSets=true"
###### Run Update-database commmand


After running the migration you have to set the following project as start up
![image](https://user-images.githubusercontent.com/20483242/187949911-eec774e0-103d-4538-ac36-f2f10d72554d.png)



# API

![image](https://user-images.githubusercontent.com/20483242/187952158-62a02a06-21ed-486d-96cb-8d3fe638f5f6.png)

# Front End

![image](https://user-images.githubusercontent.com/20483242/187952426-e02798aa-8374-48e8-a020-c4c2ee0173cc.png)
![image](https://user-images.githubusercontent.com/20483242/187953111-1278b4d9-ae7c-4dee-8488-c7d8740227f4.png)


# backOffice

![image](https://user-images.githubusercontent.com/20483242/187952605-7381c50c-98cb-45f8-abe1-5ce93bf21654.png)
![image](https://user-images.githubusercontent.com/20483242/187952698-3aad4696-c479-47de-a78c-813020715d44.png)
![image](https://user-images.githubusercontent.com/20483242/187953209-1c5392fe-19dd-49f5-b9a0-a94cb238355b.png)


# Login

![image](https://user-images.githubusercontent.com/20483242/187952820-ec4ad49e-6c4c-4bfb-81c6-832419d318c9.png)





