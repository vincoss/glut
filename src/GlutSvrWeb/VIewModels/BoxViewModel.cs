using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.VIewModels
{
    public class BoxViewModel<T> where T: class
    {
        public string Title { get; set; }
        public T Model { get; set; }
    }
}
