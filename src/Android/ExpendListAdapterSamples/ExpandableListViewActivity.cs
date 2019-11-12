using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExpendListAdapterSamples.Controls;

namespace ExpendListAdapterSamples
{
    [Activity(Label = "ExpandableListViewActivity")]
    public class ExpandableListViewActivity : Activity
    {
        Dictionary<string, List<string>> dictGroup = new Dictionary<string, List<string>>();
        List<string> lstKeys = new List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ExpandableListView);

            CreateExpendableListData();

            var ctlExListBox = FindViewById<ExpandableListView>(Resource.Id.ctlExListBox);
            ctlExListBox.SetAdapter(new ExpendListAdapter(this, dictGroup));

            ctlExListBox.ChildClick += delegate(object sender, ExpandableListView.ChildClickEventArgs e)
            {
                var itmGroup = lstKeys[e.GroupPosition];
                var itmChild = dictGroup[itmGroup][e.ChildPosition];

                Toast.MakeText(this, string.Format("You Click on Group {0} with child {1}", itmGroup, itmChild),
                                ToastLength.Long).Show();
            };
        }

       private void CreateExpendableListData()
        {
            for (int iGroup = 1; iGroup <= 3; iGroup++)
            {
                var lstChild = new List<string>();
                for (int iChild = 1; iChild <= 3; iChild++)
                {
                    lstChild.Add(string.Format("Group {0} Child {1}", iGroup, iChild));
                }
                dictGroup.Add(string.Format("Group {0}", iGroup), lstChild);
            }
            lstKeys = new List<string>(dictGroup.Keys);
        }


      
    }
}