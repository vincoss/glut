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
using Android.Views.Animations;

namespace AnimationSample
{
    [Activity(Label = "ViewAnimationActivity")]
    public class ViewAnimationActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ViewAnimation);

            FindViewById<ImageView>(Resource.Id.ImageShow).SetImageResource(Resource.Drawable.Ship);
            Button button = FindViewById<Button>(Resource.Id.ButtonStart);

            button.Click += (sender, args) =>
            {
                Animation hyperspaceAnimation = AnimationUtils.LoadAnimation(this, Resource.Animator.hyperspace);
                FindViewById<ImageView>(Resource.Id.ImageShow).StartAnimation(hyperspaceAnimation);
            };
        }
    }
}