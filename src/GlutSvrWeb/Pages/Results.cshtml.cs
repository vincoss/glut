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
    public class ResultsModel : PageModel
    {
        private readonly IDataStoreSvr _dataStoreSvr;

        public ResultsModel(IDataStoreSvr dataStoreSvr)
        {
            if(dataStoreSvr == null)
            {
                throw new ArgumentNullException(nameof(dataStoreSvr));
            }
            _dataStoreSvr = dataStoreSvr;
        }

        public void OnGet()
        {
            Project = "Glut";
            RunId = 4;
        }

        [BindProperty]
        public string Project { get; set; }

        [BindProperty]
        public int RunId { get; set; }

        public IEnumerable<ResultItemDto> Results { get; private set; }

        public string Title
        {
            get { return $"{AppResources.Results}"; }
        }
    }
}
