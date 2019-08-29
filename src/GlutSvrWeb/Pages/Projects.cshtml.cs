using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlutSvrWeb.Dto;
using GlutSvrWeb.Interfaces;
using GlutSvrWeb.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public void OnGet()
        {
        }

        public IEnumerable<ProjectDto> Projects { get; private set; }

        public string Title
        {
            get { return $"{AppResources.Projects}"; }
        }
    }
}
