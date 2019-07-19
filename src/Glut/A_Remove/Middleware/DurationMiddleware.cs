using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Glut.Middleware
{
    // TODO:
    public class DurationMiddleware
    {
        private readonly RequestDelegate _next;

        public DurationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(RequestContext context)
        {
            // do process and return

            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }
}
