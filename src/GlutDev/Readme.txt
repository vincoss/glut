
# Cleaning 
F:\_VincoHome\Business-Job\Bills-Receipts\2013

# Tasks
+ApplicationName is composite including versin number Glut 1.0.0.0
+length value form response ensure that that is with headers and content. ThreadResult does that
+the duration request must enforce or create new CancelationToken
+add sn key
Ensure that request contains 'User-Agent' glut if missing, possible other also
Totals, Add Max,Min,Arg
Store the Run setting into the database
favicon icon create and add into the project
navigation the item is not selected see CSS or JS script
CLI output per line with colours like for dashboard
antiforgery <!-- TODO: Move to master page and check if can have one per whole app -->
+dashboard header display % sign
1. Last 10 runs Total success requests
2. Last 10 runs Total error requests
runner should not cancel already running if duration expires just dont create new request and wait for already running requets
lenght for ui should be string Kb
cli colour code totals like a dasbhoard header
box height need to be set. see pi-hole for page width and box height settings


#ProjectRun
	ProjectName
	RunId
	Threads
	Count
	Duraction


# Nice to have
Request timeout not implemented
Capture repsonse headers to .txt file parser should be able to parse if required
SocketService will be required
interval option this can be used with duration
	interval make request every specified interval
	duration for specified duration
add exit code result based on status code filter list
ResponseContentPath Save the response content into specified directory, must not delete on close the stream. Nice to have
buy me a coffee


Test fore all return HTTP every codes, create samples
 // TODO: Those below are not right, since the test file can provide those headers and need to use those instead of default ones.
                // client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                // client.DefaultRequestHeaders.Add("User-Agent", Extensions.GetDefaultApplicatioName(_appConfig.ApplicationName));

				request URLS and heades should be initially read when requied
	those need to be later cached no need to read|parse those multiple times

/*
       Url  Count   Success StatusCode  MinLength ManLength AvgLength  MinRequestTicks  MaxRequestTicks AvgRequestTicks MinResponseTicks  MaxResponseTicks AvgResponseTicks
       /    50      1       200         200       500       350

        #Store
        MAX|MIN for length and Request|Respone ticks
        AVG for length, request|response ticks

     */

- Validate AppConfig values

#Tables
	Projects
		ProjectId
		ProjectName
		CreatedDateTimeUtc
		Threads
		Count
		Duration
		Interval
		StartDateTimeUtc
		EndDateTimeUtc


# Ef migrations
1.
add-migration InitialCreate -verbose

# Charts
4x charts by total requests, status code200, 400, 500
PieChart by status code


# UI examples

Data tables
	https://adminlte.io/themes/AdminLTE/pages/tables/data.html
	https://adminlte.io/
	https://startbootstrap.com/themes/clean-blog/

Icon
https://www.compart.com/en/unicode/U+2305
https://en.wikipedia.org/wiki/List_of_Unicode_characters

# UI Order
 top header status codes
https://adminlte.io/themes/AdminLTE/pages/UI/general.html
drop down list
https://adminlte.io/themes/AdminLTE/pages/forms/advanced.html
pie chart
https://adminlte.io/themes/AdminLTE/pages/charts/chartjs.html


	 
check async System.Threading.Tasks.ValueTask
check create raw web request
check HttpRequestMessage, headers and content
Concurrency in C# Cookbook
fileWatch should reload the messages
https://developer.mozilla.org/en-US/docs/Web/HTTP/Messages
https://stackoverflow.com/questions/13255622/parsing-raw-http-request
http://www.java2s.com/Code/Java/Network-Protocol/HttpParser.htm
https://tools.ietf.org/html/rfc2616#section-5
https://code.joejag.com/2012/how-to-send-a-raw-http-request-via-java.html

#Resources
https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2
https://thomaslevesque.com/tag/httpclient/
https://johnthiriet.com/efficient-api-calls/#
https://www.c-sharpcorner.com/article/measuring-and-reporting-the-response-time-of-an-asp-net-core-api/
https://gist.github.com/thomaslevesque/b4fd8c3aa332c9582a57935d6ed3406f
https://devhints.io/moment