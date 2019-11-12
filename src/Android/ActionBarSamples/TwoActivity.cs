using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;


namespace ActionBarSamples
{
    [Activity(Label = "ActionBarActivity")]
    public class TwoActivity : Activity
    {
        private static readonly string Tag = "ActionBarTabsSupport";

        private Fragment[] _fragments;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.TwoView);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;


            _fragments = new Fragment[]
                         {
                             new WhatsOnFragment(),
                             new SpeakersFragment(),
                             new SessionsFragment()
                         };

            AddTabToActionBar(Resource.String.whatson_tab_label, Resource.Drawable.ic_action_whats_on);
            AddTabToActionBar(Resource.String.speakers_tab_label, Resource.Drawable.ic_action_speakers);
            AddTabToActionBar(Resource.String.sessions_tab_label, Resource.Drawable.ic_action_sessions);
        }

        void AddTabToActionBar(int labelResourceId, int iconResourceId)
        {
            ActionBar.Tab tab = ActionBar.NewTab()
                                         .SetText(labelResourceId)
                                         .SetIcon(iconResourceId);
            tab.TabSelected += TabOnTabSelected;
            ActionBar.AddTab(tab);
        }

        void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        {
            ActionBar.Tab tab = (ActionBar.Tab)sender;

            Log.Debug(Tag, "The tab {0} has been selected.", tab.Text);
            Fragment frag = _fragments[tab.Position];
            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }
    }

    public class SpeakersFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.TwoFragmentView, null);
            view.FindViewById<TextView>(Resource.Id.textView1).SetText(Resource.String.speakers_tab_label);
            view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(Resource.Drawable.ic_action_speakers);
            return view;
        }
    }

    public class SessionsFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.TwoFragmentView, null);
            view.FindViewById<TextView>(Resource.Id.textView1).SetText(Resource.String.sessions_tab_label);
            view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(Resource.Drawable.ic_action_sessions);
            return view;
        }
    }

    public class WhatsOnFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.TwoFragmentView, null);
            view.FindViewById<TextView>(Resource.Id.textView1).SetText(Resource.String.whatson_tab_label);
            view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(Resource.Drawable.ic_action_whats_on);
            return view;
        }
    }
}