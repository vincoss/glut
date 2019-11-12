using System;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;

using Android.App;
using Android.OS;
using Android.Widget;
using System.Json;


namespace HandlingRotationSample
{
    [Activity(Label = "NonConfigInstanceActivity")]
    public class NonConfigInstanceActivity : ListActivity
    {
        private TweetListWrapper _savedInstance;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Retrieve last configuration saved on OnRetainNonConfigurationInstance method.
            var tweetsWrapper = LastNonConfigurationInstance as TweetListWrapper;

            if (tweetsWrapper != null)
            {
                PopulateTweetList(tweetsWrapper.Tweets);
            }
            else
            {
                SearchTwitter("xamarin");
            }
        }

        public override Java.Lang.Object OnRetainNonConfigurationInstance()
        {
            base.OnRetainNonConfigurationInstance();
            return _savedInstance;
        }

        public void SearchTwitter(string text)
        {
            Console.WriteLine("call twitter");

            var twitter = new Twitter();
            ParseResults(twitter.GetTweets());
        }

        private void ParseResults(string jsonData)
        {
            var j = (JsonObject)JsonObject.Parse(jsonData);

            var results = (from result in (JsonArray)j["statuses"]
                           let jResult = result as JsonObject
                           select jResult["text"].ToString()).ToArray();

            RunOnUiThread(() =>
            {
                PopulateTweetList(results);
            });
        }

        void PopulateTweetList(string[] results)
        {
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.ItemView, results);
            _savedInstance = new TweetListWrapper { Tweets = results };
        }

        class TweetListWrapper : Java.Lang.Object
        {
            public string[] Tweets { get; set; }
        }
    }

    public class TwitterAuthenticationResponse
    {
        public string TokenType { get; set; }

        public string AccessToken { get; set; }
    }

    public class Twitter
    {
        public const string OAuthConsumerKey = "onoOUsBbjVWc8msLyuRJQ";
        public const string OAuthConsumerSecret = "Vxh0zMFRAWp2LbkwfDNvUKU2dQhaVgFFv3M04gDKFE";
        public const string OAuthUrl = "https://api.twitter.com/oauth2/token";
        public const string TwitterUrl = "https://api.twitter.com/1.1/search/tweets.json?q=%40xamarin&show-user=true&count=40&result-type=recent";

        private string _authHeaderFormat = "Basic {0}";
        private HttpWebRequest _authRequest;

        public bool Authenticated { get; private set; }

        public TwitterAuthenticationResponse AuthenticationInfo { get; private set; }

        private string AuthHeader
        {
            get
            {
                byte[] authHeaderData = Encoding.UTF8.GetBytes(Uri.EscapeDataString(OAuthConsumerKey) + ":" +
                                        Uri.EscapeDataString(OAuthConsumerSecret));

                return string.Format(_authHeaderFormat, Convert.ToBase64String(authHeaderData));
            }
        }

        private WebRequest AuthenticationRequest
        {
            get
            {
                if (_authRequest == null)
                {
                    _authRequest = (HttpWebRequest)WebRequest.Create(OAuthUrl);
                    _authRequest.Headers.Add("Authorization", AuthHeader);
                    _authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                    _authRequest.Method = "POST";
                    _authRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    _authRequest.Headers.Add("Accept-Encoding", "gzip");
                    _authRequest.Timeout = 10000;
                    _authRequest.KeepAlive = true;

                    using (var stream = _authRequest.GetRequestStream())
                    {
                        byte[] content = ASCIIEncoding.ASCII.GetBytes("grant_type=client_credentials");
                        stream.Write(content, 0, content.Length);
                    }
                }

                return _authRequest;
            }
        }

        public Twitter()
        {
            Authorize();
        }

        public string GetTweets()
        {
            var webClient = new WebClient();
            System.Diagnostics.Debug.WriteLine("Get remote json data");

            webClient.Headers.Set("Authorization", string.Format("{0} {1}", AuthenticationInfo.TokenType,
                    AuthenticationInfo.AccessToken));
            webClient.Encoding = System.Text.Encoding.UTF8;
            return webClient.DownloadString(new Uri(TwitterUrl));
        }

        private void Authorize()
        {
            try
            {
                var authResponse = AuthenticationRequest.GetResponse();
                using (var reader = new StreamReader(authResponse.GetResponseStream()))
                {
                    var authenticationInfo = ParseAuthenticationResponse(reader.ReadToEnd());
                    if (!string.IsNullOrEmpty(authenticationInfo.AccessToken) &&
                        !string.IsNullOrEmpty(authenticationInfo.TokenType))
                    {
                        Authenticated = true;
                        AuthenticationInfo = authenticationInfo;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Twitter authentication error occured");
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Twitter authentication error: {0}", e.Message);
            }
        }

        private TwitterAuthenticationResponse ParseAuthenticationResponse(string tokenInfo)
        {
            if (string.IsNullOrEmpty(tokenInfo))
            {
                throw new ArgumentNullException("Twitter authentication error, token can't be null or emty string");
            }

            var value = JsonValue.Parse(tokenInfo);
            return new TwitterAuthenticationResponse()
            {
                TokenType = value["token_type"],
                AccessToken = value["access_token"]
            };
        }
    }
}