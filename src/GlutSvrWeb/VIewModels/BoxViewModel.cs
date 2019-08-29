using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.ViewModels
{
    public class BoxViewModel<T> where T: class
    {
        public string Title { get; set; }
        public T Model { get; set; }
    }
}
