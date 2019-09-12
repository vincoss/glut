using GlutSvrWeb.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace GlutSvrWeb.Pages
{
    public class ResultsModel : PageModel
    {
        public void OnGet()
        {
            Project = "Glut"; // Todo remove use from drop down list
            RunId = 1; // TODO:
        }

        [BindProperty]
        public string Project { get; set; }

        [BindProperty]
        public int RunId { get; set; }

        public string Title
        {
            get { return $"{AppResources.Results}"; }
        }
    }
}
