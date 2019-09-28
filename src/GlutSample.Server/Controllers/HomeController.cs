using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlutSample.Server.Controllers
{
    public class HomeController : Controller
    {
        #region 100 - Information

        #endregion

        #region 200 - Successful

        // GET: /<controller>/
        public IActionResult Index(int? id)
        {
            return View(id);
        }

        /// <summary>
        /// 204
        /// </summary>
        public IActionResult NoContentTest()
        {
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        public IActionResult LongRunningTest()
        {
            System.Threading.Thread.Sleep(60000);
            return Ok();
        }

        public async Task LargeRequest(int? id)
        {
            if (id == null)
            {
                id = 100;
            }

            Response.ContentType = "text/plain";
            Response.Headers[HeaderNames.CacheControl] = "no-cache";

            var bytes = Encoding.UTF8.GetBytes(new string('z', id.Value));
            await Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }

        #endregion

        #region 300 - Redirection

        /// <summary>
        /// 304
        /// </summary>
        public IActionResult NotModifiedTest()
        {
            return StatusCode((int)HttpStatusCode.NotModified);
        }

        #endregion

        #region 400 - Client error

        /// <summary>
        /// 400
        /// </summary>
        public IActionResult TestBadRequest()
        {
            return BadRequest();
        }

        /// <summary>
        /// 403
        /// </summary>
        public IActionResult TestForbid()
        {
            return StatusCode((int)HttpStatusCode.Forbidden);
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

        #endregion

        #region 500 - Server error

        /// <summary>
        /// 500
        /// </summary>
        public void Error()
        {
            throw new Exception("This is bad error!!!");
        }

        #endregion

        #region 200 - Successful - Post

        [HttpPost]
        public int Add(int? id)
        {
            if(id == null)
            {
                return 0;
            }
            return id.Value;
        }

        #endregion

    }
}
