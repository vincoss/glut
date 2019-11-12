using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Framework;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AnimationSample
{
    [Activity(Label = "Animations Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        private List<SampleActivityDescriptor> _activities;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _activities = new List<SampleActivityDescriptor>
                {
                    new SampleActivityDescriptor(Resources.GetString(Resource.String.ShapeDrawable), typeof (DrawableActivity)),
                    new SampleActivityDescriptor(Resources.GetString(Resource.String.DrawingActivity), typeof (DrawingActivity)),
                    new SampleActivityDescriptor(Resources.GetString(Resource.String.ViewAnimation), typeof (ViewAnimationActivity)),
                    new SampleActivityDescriptor(Resources.GetString(Resource.String.PropertyAnimation), typeof (PropertyAnimationActivity)),
                    new SampleActivityDescriptor(Resources.GetString(Resource.String.AnimationDrawable), typeof (AnimationDrawableActivity)),
                    new SampleActivityDescriptor(Resources.GetString(Resource.String.ValueAnimation), typeof (ValueAnimatorActivity)),
                    new SampleActivityDescriptor(Resources.GetString(Resource.String.ZoomActivity), typeof (ZoomActivity))
                };

            ListAdapter = new ArrayAdapter<SampleActivityDescriptor>(this,
                                                           Android.Resource.Layout.SimpleListItem1,
                                                           Android.Resource.Id.Text1,
                                                           _activities);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            SampleActivityDescriptor sample = _activities[position];
            sample.Start(this);
        }

        public class SampleActivityDescriptor
        {
            public SampleActivityDescriptor(string title, Type activityToLaunch)
            {
                Title = title;
                ActivityToLaunch = activityToLaunch;
            }

            private Type ActivityToLaunch { get; set; }

            private string Title { get; set; }

            public void Start(Activity context)
            {
                Intent i = new Intent(context, ActivityToLaunch);
                context.StartActivity(i);
            }

            public override string ToString()
            {
                return Title;
            }
        }
    }
}