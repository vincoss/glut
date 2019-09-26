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

```json

{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AppConfig": {
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
  "ConnectionStrings": {
    "EfDbContext": "Data Source=C:\\Temp\\Glut\\Glut.db"
  }
}

```
### Test request data files.

* The test files are orderd by name before running the test.
* Single request test file can be used only for single request, but can sepecify HTTP attributes for the request.
* List request test file can specify number of GET requests with relative or full URL (use full URL if BaseAddress in AppConfig not set).

#### Single test file example. NOTE: not implemented

```txt

GET http://localhost:5000/home/index HTTP/1.1
Host: localhost:5000
Connection: keep-alive
Cache-Control: max-age=0
Upgrade-Insecure-Requests: 1
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.103 Safari/537.36
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.9

```

#### List test file example.

```txt

/home
/home/NoContentTest
/home/LargeRequest
/home/LargeRequest/1000
/home/LargeRequest/100000
/home/LargeRequest/10000000
/home/NotModifiedTest
/home/TestBadRequest
/home/TestForbid
/home/TestUnauthorized
/home/Timeout
/home/Error
/home/LongRunningTest

```

### Run load test with GlutCli.exe

dotnet GlutCli.dll

## How to Engage, Contribute, and Give Feedback

Some of the best ways to contribute are to try things out, file issues, join in design conversations, and make pull-requests.

## Reporting bugs

https://github.com/vincoss/glut/issues

