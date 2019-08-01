using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlutSvr.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GlutSvr.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public string Title
        {
            get { return $"{AppResources.Home}"; }
        }
    }
}
