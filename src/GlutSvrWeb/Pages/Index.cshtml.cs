using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlutSvrWeb.Interfaces;
using GlutSvrWeb.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace GlutSvrWeb.Pages
{
    public class IndexModel : PageModel
    {
        public string Title
        {
            get { return $"{AppResources.Home}"; }
        }
    }
}
