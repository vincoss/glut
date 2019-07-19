using System;
using System.Threading.Tasks;


namespace Glut.Middleware
{
    // TODO:
    public class CountMiddleware
    {
        private readonly RequestDelegate _next;

        public CountMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(RequestContext context)
        {
            if(context.Count > 0)
            {
                // do process and return
                return Task.CompletedTask; // TODO: check for the correct implementation
            }

            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }

}
