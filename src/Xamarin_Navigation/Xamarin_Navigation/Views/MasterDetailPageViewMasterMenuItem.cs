using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin_Navigation.Views
{

    public class MasterDetailPageViewMasterMenuItem
    {
        public MasterDetailPageViewMasterMenuItem()
        {
            TargetType = typeof(MasterDetailPageViewMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}