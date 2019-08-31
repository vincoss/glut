using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.Dto
{
    public class DataTableDto<T> where T: class
    {
        public string Draw { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
