using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlutSample.Server.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(int? id)
        {
            return View(id);
        }

        public IActionResult TestBadRequest()
        {
            return BadRequest();
        }

        public IActionResult TestForbid()
        {
            return Forbid();
        }

        /// <summary>
        /// 401
        /// </summary>
        public IActionResult TestUnauthorized()
        {
            return Unauthorized();
        }

        /// <summary>
        /// 408
        /// </summary>
        public IActionResult Timeout()
        {
            return StatusCode((int)HttpStatusCode.RequestTimeout);
        }

        /// <summary>
        /// 204
        /// </summary>
        public void NoContentTest()
        {
        }

        public HttpResponseMessage NotModifiedTest()
        {
            return new HttpResponseMessage(HttpStatusCode.NotModified);
        }

       
        //public void Error()
        //{
        //    throw new HttpResponseException(HttpStatusCode.NotImplemented);
        //}

        public IActionResult NeverEndingStream()
        {
            var stream = new MemoryStream();
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.Write("Hello World");
                streamWriter.Flush();
            }
            return new FileStreamResult(stream, "text/plain");
        }

        public IActionResult LongRunningTest()
        {
            System.Threading.Thread.Sleep(10000000);
            return Ok();
        }
    }
}
