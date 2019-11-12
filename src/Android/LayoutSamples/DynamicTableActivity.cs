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

namespace LinearAndRelativeLayoutSample
{
    [Activity(Label = "DynamicTableActivity")]
    public class DynamicTableActivity : Activity
    {
        private TableLayout table;

        private List<string> list_name;

        private int color_blue = -16776961;
        private int color_gray = -7829368;
        private int color_black = -16777216;
        private int color_white = -1;

        private int CHECK_BUTTON_ID = 982301;

        private int[] ids_check;
        private bool[] bool_check;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.DynamicTable);
            table = FindViewById<TableLayout>(Resource.Id.DynamicTableLayout);

            list_name = new List<string>();

            list_name.Add("Close");
            list_name.Add("One");
            list_name.Add("Two");
            list_name.Add("Three");
            list_name.Add("Four");
            list_name.Add("Five");
            list_name.Add("Six");

            ids_check = new int[list_name.Count];
            bool_check = new bool[list_name.Count];

            CreateTableRows();
        }

        public void CreateTableRows()
        {
            for (int i = 0; i < list_name.Count; i++)
            {
                TableRow table_row = new TableRow(this);
                TextView tv_name = new TextView(this);
                Button btn_check = new Button(this);
                ImageView img_line = new ImageView(this);

                table.StretchAllColumns = true;
                table.ShrinkAllColumns = true;

                //table_row.setLayoutParams(new LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT));
                table_row.SetBackgroundColor(new Color(0, 0, 0));
                table_row.SetGravity(GravityFlags.CenterHorizontal);

                tv_name.Text = list_name[i];
                tv_name.SetTextColor(new Color(0, 255, 0));
                tv_name.SetTextSize(ComplexUnitType.Dip, 16);
                tv_name.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);
                tv_name.SetWidth(150);

                //btn_check.setLayoutParams(new LayoutParams(30, 30));
                btn_check.SetBackgroundColor(new Color(192, 192, 192));

                //img_line.setLayoutParams(new LayoutParams(LayoutParams.FILL_PARENT, 2));
                img_line.SetBackgroundColor(new Color(255, 0, 0));

                table_row.AddView(tv_name);
                table_row.AddView(btn_check);

                table.AddView(table_row);
                table.AddView(img_line);

                int id = i + CHECK_BUTTON_ID;
                btn_check.Id = id;
                ids_check[i] = id;

                btn_check.Click += (s, e) =>
                    {
                        var v = (View) s;

                        for (int j = 0; j < ids_check.Length; j++)
                        {
                            Button btn_check_1 = (Button) FindViewById<Button>(ids_check[j]);
                            if (v.Id == ids_check[j])
                                if (bool_check[j])
                                {
                                    btn_check_1.SetBackgroundColor(new Color(0, 255, 0));
                                    bool_check[j] = false;
                                }
                                else
                                {
                                    btn_check_1.SetBackgroundColor(new Color(255, 0, 0));
                                    bool_check[j] = true;
                                }
                        }


                    };
            }

        }
    }
}