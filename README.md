# ![Logo](/src/GlutDev/Img/Logo.png?raw=true)  Glut

GlutCli HTTP load testing tool and library.
GlutSvr is an open-source and cross-platform framework for load testing web applications.

### Installation
To install GlutCli via [NuGet](http://www.nuget.org/packages/GlutCli), run the following command in the Package Manager Console
```
Install-Package GlutCli
```

### Docker Glut Server
To pull GlutSrv via [Docker](https://hub.docker.com/repository/docker/vincoss/glutsvr), run the following command in the Command Line or Powershell
```
docker pull vincoss/glutsvr:1.0.0
```

## Screenshots

#### Glut Web Dashboard
# ![GlutSvrWeb](/src/GlutDev/Img/Dashboard.png?raw=true)

#### GlutCli output
# ![GlutCli](/src/GlutDev/Img/Cli.png?raw=true)

## Get Started

### GlutCli - AppConfig (appsettings.json)

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
IntervalMilliseconds 	| Run request every milliseconds.
ProjectName 			| Test project name.
ProjectRunId 			| Test project run id. Set only if you want to run multiple GlutCli runers and save results against same project and runID. Keep default to '0' will automatically increament during data persistence.
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
* Single request test file can be used only for single request, and can sepecify HTTP attributes for the request.GET, POST and many other.
* List request test file can specify number of GET requests with relative or full URL (use full URL if BaseAddress in AppConfig not set).
* List requests are executed in order defined the the file.

#### Single test file example.

```txt
POST home/add/1 HTTP/1.1
User-Agent: GlutCli
Host: localhost:5000
Content-Length: 0
```

```txt
GET http://localhost:5000/home/index HTTP/1.1
Host: localhost:5000
Connection: keep-alive
Cache-Control: max-age=0
Upgrade-Insecure-Requests: 1
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64)
Accept: text/html,application/xhtml+xml,application/xml;
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

## Run load test with GlutCli.exe

* dotnet GlutCli.dll
* GlutCli.exe

## How to Engage, Contribute, and Give Feedback

Some of the best ways to contribute are to try things out, file issues, join in design conversations, and make pull-requests.

## Reporting bugs

https://github.com/vincoss/glut/issues
