using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlutSvrWeb.Dto;
using GlutSvrWeb.Interfaces;
using GlutSvrWeb.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataTables.Queryable;

namespace GlutSvrWeb.Pages
{
    public class ProjectsModel : PageModel
    {
        private readonly IDataStoreSvr _dataStoreSvr;

        public ProjectsModel(IDataStoreSvr dataStoreSvr)
        {
            if(dataStoreSvr == null)
            {
                throw new ArgumentNullException(nameof(dataStoreSvr));
            }
            _dataStoreSvr = dataStoreSvr;
        }

        public async Task OnGet()
        {
            Projects = await _dataStoreSvr.GetProjects();
        }

        public IEnumerable<ProjectDto> Projects { get; private set; }

        public string Title
        {
            get { return $"{AppResources.Projects}"; }
        }

      
        public async Task<IActionResult> OnPostGeoLocation()
        {
            var request = await _dataStoreSvr.GetProjects();
            return new JsonResult(request);
        }
    }
}
