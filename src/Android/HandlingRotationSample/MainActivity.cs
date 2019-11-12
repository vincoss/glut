using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HandlingRotationSample
{
    [Activity(Label = "HandlingRotationSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add(Menu.None, 0, Menu.None, Resource.String.PreventRestart);
            menu.Add(Menu.None, 1, Menu.None, Resource.String.ProgramaticLayout);
            menu.Add(Menu.None, 2, Menu.None, Resource.String.ProgramaticOrientation);
            menu.Add(Menu.None, 3, Menu.None, Resource.String.NonConfigurationState);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = null;

            switch (item.ItemId)
            {
                case 0:
                    intent = new Intent(this, typeof(PreventingActivityRestartActivity));
                    break;
                case 1:
                    intent = new Intent(this, typeof(ProgramaticLayoutActivity));
                    break;
                case 2:
                    intent = new Intent(this, typeof(ProgramaticOrientationActivity));
                    break;
                case 3:
                    intent = new Intent(this, typeof(NonConfigInstanceActivity));
                    break;
            }

            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }
    }
}

