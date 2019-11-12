using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Net;
using Flurl.Http;


namespace SelfHostedOwinWebApi_Client_AndroidSample
{
    [Activity(Label = "SelfHostedOwinWebApi_Client_AndroidSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView _detailTextView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.FindCompaniesButton);
            _detailTextView = FindViewById<TextView>(Resource.Id.DetailTextView);

            button.Click += button_Click;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Progress("Button clicked");

            try
            {
                var result = SearchCompanies();

                Progress(result.Result);
            }
            catch (Exception ex)
            {
                Progress(ex.ToString());
            }
        }

        private async Task<string> SearchCompanies()
        {
            Progress("SearchCompanies");

            try
            {
                var result = await "http://www.google.com.au".GetStringAsync();
                return result;
            }
            catch (Exception ex)
            {
                Progress(ex.ToString());
            }

            return "WRONG";


            //string url = "http://10.0.0.25:8080/api/company/get/";
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            //request.ContentType = "application/json";
            //request.Method = "GET";

            //Progress(url);

            //// Send the request to the server and wait for the response:
            //using (WebResponse response = await request.GetResponseAsync())
            //{
            //    Progress("With response");

            //    return null;

            //    //// Get a stream representation of the HTTP web response:
            //    //using (Stream stream = response.GetResponseStream())
            //    //{
            //    //    Progress("With stream");

            //    //    // Use this stream to build a JSON document object:
            //    //    System.Json.JsonValue jsonDoc = await Task.Run(() => System.Json.JsonObject.Load(stream));
            //    //    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

            //    //    // Return the JSON document:
            //    //    return jsonDoc;
            //    //}
            //}
        }

        private void Progress(string message)
        {
            RunOnUiThread(() => _detailTextView.Text = string.Format("{0}{1}{2}", _detailTextView.Text, System.Environment.NewLine, message));
        }
    }
}

