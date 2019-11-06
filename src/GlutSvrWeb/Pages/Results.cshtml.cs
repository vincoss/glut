using GlutSvrWeb.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace GlutSvrWeb.Pages
{
    public class ResultsModel : PageModel
    {
        public void OnGet()
        {
        }

        public string Title
        {
            get { return $"{AppResources.Results}"; }
        }
    }
}
