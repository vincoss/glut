using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;


namespace ActionBarSamples
{
    [Activity(Label = "ThreeActivity")]
    public class ThreeActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.ThreeView);

            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            ActionBar.Tab tab = ActionBar.NewTab();
            tab.SetText(Resources.GetString(Resource.String.Home));
            tab.SetIcon(Resource.Drawable.Icon);
            tab.SetTabListener(new TabListener<HomeFragment>(this, "Home"));
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetText(Resources.GetString(Resource.String.Games));
            tab.SetIcon(Resource.Drawable.Icon);
            tab.SetTabListener(new TabListener<GamesFragment>(this, "Games"));

            ActionBar.AddTab(tab);
        }
    }

    public class HomeFragment : Fragment
    {
        public HomeFragment() : base() { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var tv = new TextView(Activity);
            tv.Text = "Home";

            return tv;
        }
    }

    public class GamesFragment : Fragment
    {
        public GamesFragment() : base() { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var tv = new TextView(Activity);
            tv.Text = "Games";

            return tv;
        }
    }

    public class TabListener<T> : Java.Lang.Object, ActionBar.ITabListener where T : Fragment
    {
        private T _fragment;
        private readonly Context _context;
        private readonly string _tag;
        private readonly string _fragmentName;

        public TabListener(Context listenerContext)
        {
            if (listenerContext == null)
            {
                throw new ArgumentNullException("context");
            }

            this._context = listenerContext;
            this._fragmentName = typeof(T).Namespace.ToLower() + "." + typeof(T).Name;
        }

        public TabListener(Context listenerContext, string tag, T existingFragment = null) : this(listenerContext)
        {
            if (tag == null)
            {
                throw new ArgumentNullException("tag");
            }
            this._fragment = existingFragment;
            this._tag = tag;
        }

        public T Fragment
        {
            get { return this._fragment; }
        }

        public void OnTabReselected(ActionBar.Tab tab, FragmentTransaction ft)
        {
            Log.Debug(this.GetType().Name, "The tab {0} was re-selected.", tab.Text);
        }

        public void OnTabSelected(ActionBar.Tab tab, FragmentTransaction ft)
        {
            Log.Debug(this.GetType().Name, "The tab {0} has been selected.", tab.Text);

            if (this._fragment == null)
            {
                this._fragment = (T) global::Android.App.Fragment.Instantiate(this._context, this._fragmentName);
                if (this._tag != null)
                {
                    ft.Add(global::Android.Resource.Id.Content, this._fragment, this._tag);
                }
                else
                {
                    ft.Add(global::Android.Resource.Id.Content, this._fragment);
                }
            }
            else
            {
                ft.Attach(this._fragment);
            }
        }

        public void OnTabUnselected(ActionBar.Tab tab, FragmentTransaction ft)
        {
            Log.Debug(this.GetType().Name, "The tab {0} has been selected.", tab.Text);

            if (this._fragment != null)
            {
                ft.Detach(this._fragment);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._fragment.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}