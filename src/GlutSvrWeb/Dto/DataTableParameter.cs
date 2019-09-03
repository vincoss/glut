using GlutSvrWeb.Services;
using System;


namespace GlutSvrWeb.Dto
{
    public class DataTableParameter
    {
        public string Draw { get; set; } // TOOD: What is this for???

        public string SortColumn { get; set; }
        public string SortDirection { get; set; } = ViewConstants.SortDirectionAsc;

        public string Search { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; } = 10;

    }
}
