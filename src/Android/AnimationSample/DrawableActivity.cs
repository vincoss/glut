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
    [Activity(Label = "ShapeDrawableActivity")]
    public class DrawableActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Drawable);
        }

        protected override void OnStart()
        {
            base.OnStart();

            // ShapeDrawable
            TextView tv = FindViewById<TextView>(Resource.Id.TextViewShapeDrawableProgramatically);
            tv.SetBackgroundResource(Resource.Drawable.gradient_box);

            // TransitionDrawable
            var transitionButton = FindViewById<ImageButton>(Resource.Id.ImageTransitionDrawable);
            var transitionDrawable = (TransitionDrawable)transitionButton.Drawable;
            transitionButton.Click += (s, e) =>
                {
                    transitionDrawable.StartTransition(1000);
                };

            // LevelListDrawable
            var levelListButton = FindViewById<ImageButton>(Resource.Id.ImageLevelListDrawable);
            var levelListDrawable = (LevelListDrawable)levelListButton.Drawable;
            levelListButton.Click += (s, e) =>
                {
                    var level = levelListDrawable.Level;
                    if (level == 3)
                    {
                        level = 0;
                    }
                    else
                    {
                        level++;
                    }
                    levelListDrawable.SetLevel(level);
                };

            // ClipDrawable
            var clipImage = FindViewById<ImageView>(Resource.Id.ClipImageView);
            var clipDrawable = (ClipDrawable)clipImage.Drawable;
            clipDrawable.SetLevel(clipDrawable.Level + 1000);
        }
    }
}