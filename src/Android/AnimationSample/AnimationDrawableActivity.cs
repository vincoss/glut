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
using Android.Graphics.Drawables;

namespace AnimationSample
{
    [Activity(Label = "AnimationDrawableActivity")]
    public class AnimationDrawableActivity : Activity
    {
        //private AnimationDrawable _asteroidDrawable;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AnimationDrawable);

            // Load the animation from resources
            //_asteroidDrawable = (AnimationDrawable)Resources.GetDrawable(Resource.Drawable.spinning_asteroid);
            var imageView = FindViewById<ImageView>(Resource.Id.ImageViewAsteroid);
            var animationDrawable = (AnimationDrawable)imageView.Drawable;
            //imageView.SetImageDrawable(_asteroidDrawable);

            Button spinAsteroidButton = FindViewById<Button>(Resource.Id.ButtonSpin);
            spinAsteroidButton.Click += (sender, args) =>
                {
                    if (animationDrawable.IsRunning == false)
                    {
                        animationDrawable.Start();
                    }
                    else
                    {
                        animationDrawable.Stop();
                    }
                };
        }
    }
}