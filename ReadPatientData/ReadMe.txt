/*****************************************************
* Solution to Programming exercise by Shamalka Herath
* Software Developer position, Maxim Software Systems
******************************************************/

Important:

1). To solve this problem, I build a small project: an ASP.NET console application using Visual Studio 2017 community version.

2). To open the project, open "readPatientData.sln" on Visual Studio and select "Start" to run the application
 
2). Now when each patient's data is recovered from the patients.dat file, DB is accessed to save each record to tblPatient. so when there are n patient records, DB will be accessed n times where n>0. This can be time consuming when have to deal with real SQL server DB, so I would consider by inserting a list of patient records instead of one by one to minimize DB access.

3). Used local DB : PatientDB via Entity Framework to insert patient records to tblPatient

Please find  the solution through following method:

    Shared folder "ReadPatientData" in Google Drive - an email will be sent to "pablo@maximsoftware.com" to grant the access to the folder 