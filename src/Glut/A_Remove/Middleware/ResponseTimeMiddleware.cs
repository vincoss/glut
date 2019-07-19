using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Glut.Middleware
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            if(next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            _next = next;
        }
        public Task InvokeAsync(RequestContext context)
        {
            throw new NotImplementedException();
            // Start the Timer using Stopwatch  
            //var watch = new Stopwatch();
            //watch.Start();
            //context.Response.OnStarting(() => {
            //    // Stop the timer information and calculate the time   
            //    watch.Stop();
            //    var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
            //    // Add the Response time information in the Response headers.   
            //    context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
            //    return Task.CompletedTask;
            //});
            //// Call the next delegate/middleware in the pipeline   
            //return this._next(context);
        }
    }
}
