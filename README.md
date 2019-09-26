# Glut
HTTP load testing tool and library.

Glut
============

Glut is an open-source and cross-platform framework for load testing web applications.

## Get Started

### AppConfig (appsettings.json)

Configure load setting for the test project.

Key| Value        
------------------------|--------------------------------------------------------
BaseAddress 			| Base URL to run load test. If not specified then list and single file must specify full URL instead of relative URL.
ListSubpath 			| Folder name for multi request test files.
SingleSubpath 			| Folder name for single request test files.
ContentRootPath 		| Root path for test files. ListSubpath and SingleSubpath must be in this folder.
Threads 				| Number of threads to run the test.
Count 					| Number of test interations. Count takes predence over DurationMilliseconds.
DurationMilliseconds 	| Run test for number of milliseconds.
IntervalMilliseconds 	| Run request every milliseconds. (NOT Implemented).
ProjectName 			| Test project name.
ProjectRunId 			| Test project run id. Set only if you want to run multiple CLI runers and save results against same project and runID.
PersistResults 			| Persist changes into the database. Results can be viewed through GlutSvr project web site.

appsettings.json example:

```json
{
  "AppConfig": 
  {
    "BaseAddress": "http://localhost:5000",
    "ListSubpath": "list",
    "SingleSubpath": "single",
    "ContentRootPath": "C:\\Temp\\Glut\\Sample\\",
    "Threads": 10,
    "Count": 0,
    "DurationMilliseconds": 10000,
    "IntervalMilliseconds": 0,
    "ProjectName": "Sample",
    "ProjectRunId": 0,
    "PersistResults": true
  },
  "ConnectionStrings": 
  {
    "EfDbContext": "Data Source=C:\\Temp\\Glut\\Glut.db"
  }
}
```

### Run load test with GlutCli.exe

dotnet GlutCli.dll

## How to Engage, Contribute, and Give Feedback

Some of the best ways to contribute are to try things out, file issues, join in design conversations, and make pull-requests.

## Reporting bugs

https://github.com/vincoss/glut/issues

