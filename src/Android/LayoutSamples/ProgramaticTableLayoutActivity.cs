using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LinearAndRelativeLayoutSample
{
    [Activity(Label = "ProgramaticTableLayoutActivity")]
    public class ProgramaticTableLayoutActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(CreateTableLayout());
        }

        private TableLayout CreateTableLayout()
        {
            TableLayout table = new TableLayout(this);

            table.StretchAllColumns = true;
            table.ShrinkAllColumns = true;

            TableRow rowTitle = new TableRow(this);
            rowTitle.SetGravity(GravityFlags.CenterHorizontal);

            TableRow rowDayLabels = new TableRow(this);
            TableRow rowHighs = new TableRow(this);
            TableRow rowLows = new TableRow(this);
            TableRow rowConditions = new TableRow(this);
            rowConditions.SetGravity(GravityFlags.Center);

            TextView empty = new TextView(this);

            // title column/row
            TextView title = new TextView(this);
            title.Text = "Xamarin Weather Table";

            title.SetTextSize(ComplexUnitType.Dip, 18);
            title.Gravity = GravityFlags.Center;
            title.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);

            TableRow.LayoutParams items = new TableRow.LayoutParams();
            items.Span = 6;

            rowTitle.AddView(title, items);

            // labels column
            TextView highsLabel = new TextView(this);
            highsLabel.Text = "Day High";
            highsLabel.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);

            TextView lowsLabel = new TextView(this);
            lowsLabel.Text = "Day Low";
            lowsLabel.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);

            TextView conditionsLabel = new TextView(this);
            conditionsLabel.Text = "Conditions";
            conditionsLabel.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);

            rowDayLabels.AddView(empty);
            rowHighs.AddView(highsLabel);
            rowLows.AddView(lowsLabel);
            rowConditions.AddView(conditionsLabel);

            // day 1 column
            TextView day1Label = new TextView(this);
            day1Label.Text = "Feb 7";
            day1Label.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);

            TextView day1High = new TextView(this);
            day1High.Text = "28°F";
            day1High.Gravity = GravityFlags.CenterHorizontal;

            TextView day1Low = new TextView(this);
            day1Low.Text = "15°F";
            day1Low.Gravity = GravityFlags.CenterHorizontal;

            TextView day1Conditions = new TextView(this);
            day1Conditions.Text = "HOT";

            rowDayLabels.AddView(day1Label);
            rowHighs.AddView(day1High);
            rowLows.AddView(day1Low);
            rowConditions.AddView(day1Conditions);

            // day2 column
            TextView day2Label = new TextView(this);
            day2Label.Text = "Feb 8";
            day2Label.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);

            TextView day2High = new TextView(this);
            day2High.Text = "26°F";
            day2High.Gravity = GravityFlags.CenterHorizontal;

            TextView day2Low = new TextView(this);
            day2Low.Text = "14°F";
            day2Low.Gravity = GravityFlags.CenterHorizontal;

            TextView day2Conditions = new TextView(this);
            day2Conditions.Text = "CLOUDY";

            rowDayLabels.AddView(day2Label);
            rowHighs.AddView(day2High);
            rowLows.AddView(day2Low);
            rowConditions.AddView(day2Conditions);

            // day3 column
            TextView day3Label = new TextView(this);
            day3Label.Text = "Feb 9";
            day3Label.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);

            TextView day3High = new TextView(this);
            day3High.Text = "23°F";
            day3High.Gravity = GravityFlags.CenterHorizontal;

            TextView day3Low = new TextView(this);
            day3Low.Text = "3°F";
            day3Low.Gravity = GravityFlags.CenterHorizontal;

            TextView day3Conditions = new TextView(this);
            day3Conditions.Text = "SNOW";

            rowDayLabels.AddView(day3Label);
            rowHighs.AddView(day3High);
            rowLows.AddView(day3Low);
            rowConditions.AddView(day3Conditions);

            // day4 column
            TextView day4Label = new TextView(this);
            day4Label.Text = "Feb 10";
            day4Label.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);

            TextView day4High = new TextView(this);
            day4High.Text = "17°F";
            day4High.Gravity = GravityFlags.CenterHorizontal;

            TextView day4Low = new TextView(this);
            day4Low.Text = "5°F";
            day4Low.Gravity = GravityFlags.CenterHorizontal;

            TextView day4Conditions = new TextView(this);
            day4Conditions.Text = "SNOW";

            rowDayLabels.AddView(day4Label);
            rowHighs.AddView(day4High);
            rowLows.AddView(day4Low);
            rowConditions.AddView(day4Conditions);

            // day5 column
            TextView day5Label = new TextView(this);
            day5Label.Text = "Feb 11";
            day5Label.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);

            TextView day5High = new TextView(this);
            day5High.Text = "19°F";
            day5High.Gravity = GravityFlags.CenterHorizontal;

            TextView day5Low = new TextView(this);
            day5Low.Text = "6°F";
            day5Low.Gravity = GravityFlags.CenterHorizontal;

            TextView day5Conditions = new TextView(this);
            day5Conditions.Text = "SUN";

            rowDayLabels.AddView(day5Label);
            rowHighs.AddView(day5High);
            rowLows.AddView(day5Low);
            rowConditions.AddView(day5Conditions);

            table.AddView(rowTitle);
            table.AddView(rowDayLabels);
            table.AddView(rowHighs);
            table.AddView(rowLows);
            table.AddView(rowConditions);

            return table;
        }
    }
}