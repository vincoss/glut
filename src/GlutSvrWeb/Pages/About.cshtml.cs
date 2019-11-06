using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlutSvrWeb.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GlutSvrWeb.Pages
{
    public class AboutModel : PageModel
    {
        public void OnGet()
        {

        }

        public string Title
        {
            get { return $"{AppResources.About}"; }
        }
    }
}
