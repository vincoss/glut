using GlutSvrWeb.Properties;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace GlutSvrWeb.Pages
{
    public class ProjectsModel : PageModel
    {

        public void OnGet()
        {
        }

        public string Title
        {
            get { return $"{AppResources.Projects}"; }
        }
    }
}
