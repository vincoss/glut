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

namespace ExpendListAdapterSamples.Controls
{
    public class ExpendListAdapter : BaseExpandableListAdapter
    {
        private IDictionary<string, List<string>> _dictGroup = null;
         private List<string> _lstGroupID = null;
         private Activity _activity;

         public ExpendListAdapter(Activity activity, IDictionary<string, List<string>> dictGroup)
         {
             _dictGroup = dictGroup;
             _activity = activity;
             _lstGroupID = dictGroup.Keys.ToList();

         }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return _dictGroup[_lstGroupID[groupPosition]][childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var item = _dictGroup[_lstGroupID[groupPosition]][childPosition];

            if (convertView == null)
                convertView = _activity.LayoutInflater.Inflate(Resource.Drawable.ExpandableListItemView, null);

            var textBox = convertView.FindViewById<TextView>(Resource.Id.txtSmall);
            textBox.SetText(item, TextView.BufferType.Normal);

            return convertView;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return _dictGroup[_lstGroupID[groupPosition]].Count;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return _lstGroupID[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var item = _lstGroupID[groupPosition];

            if (convertView == null)
            {
                convertView = _activity.LayoutInflater.Inflate(Resource.Drawable.ExpandableListGroupView, null);
            }
            var textBox = convertView.FindViewById<TextView>(Resource.Id.txtLarge);
            textBox.SetText(item, TextView.BufferType.Normal);

            return convertView;
        }

        public override int GroupCount
        {
            get { return _dictGroup.Count; }
        }

        public override bool HasStableIds
        {
            get { return true; }
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}