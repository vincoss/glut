using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace LinearAndRelativeLayoutSample.Controls
{
    public class CustomLayout : ViewGroup
    {
        private static String TAG = "CustomLayout";

        private static int NUM_CHILDREN = 20;
        private static int GRID_WIDTH = 7;
        private static int GRID_HEIGHT = 12;

        private static float[] COORDS =
            {
                // row 0
                0, 0, 7, 1,
                // row 1
                0, 1, 1, 1,
                1, 1, 2, 1,
                3, 1, 2, 1,
                5, 1, 2, 1,
                // row 2
                0, 2, 1, 1,
                1, 2, 1, 1,
                2, 2, 1, 1,
                3, 2, 1, 1,
                4, 2, 1, 1,
                5, 2, 1, 1,
                6, 2, 1, 1,
                // row 3
                0, 3, 1, 7,
                1, 3, 5, 7,
                6, 3, 1, 7,
                // row 4
                0, 10, 1, 1,
                1, 10, 3, 1,
                4, 10, 2, 1,
                6, 10, 1, 1,
                // row 5
                0, 11, 7, 1
            };

        public CustomLayout(Context context) : base(context)
        {
            Init();
        }

        public CustomLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        private void Init()
        {
            Context ctx = this.Context;
            SetBackgroundColor(new Color(255, 0, 0));
            for (int i = 0; i < NUM_CHILDREN; i++)
            {
                // TODO: Add your content in here

                TextView tv = new Button(ctx);
                tv.Text = string.Format("{0}/7", i);
                
                AddView(tv);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            int w = this.Width;
            int h = this.Height;
            for (int i = 0; i < NUM_CHILDREN; i++)
            {
                View v = GetChildAt(i);
                int ii = i * 4;
                float fl = w * COORDS[ii] / GRID_WIDTH;
                float ft = h * COORDS[ii + 1] / GRID_HEIGHT;
                float fr = fl + w * COORDS[ii + 2] / GRID_WIDTH;
                float fb = ft + h * COORDS[ii + 3] / GRID_HEIGHT;
                Log.Debug(TAG, "onLayout " + fl + " " + ft + " " + fr + " " + fb);
                v.Layout((int)Math.Round(fl), (int)Math.Round(ft), (int)Math.Round(fr), (int)Math.Round(fb));
            }
        }
    }
}