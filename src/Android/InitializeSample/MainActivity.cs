using System;
using Android.App;
using Android.Widget;
using Android.OS;


namespace InitializeSample
{
    [Activity(Label = "InitializeSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private readonly MainActivityViewModel _model;
        private TextView _textView;

        public MainActivity() : this(new MainActivityViewModel())
        {
        }

        public MainActivity(MainActivityViewModel model)
        {
            _model = model;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            _textView = FindViewById<TextView>(Resource.Id.TextViewDetail);
        }

        /// <summary>
        /// Because this is the only method guaranteed to be called when an activity
        /// is being activated (after the initilization), we need to make sure that 
        /// we undo anything that we might have done in OnPause.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            // NOTE: Initialize under restore since this method is quarenteed to be excecuted.
            this._model.Initialize();

            this._textView.Text = _model.Message;
        }
    }

    public class MainActivityViewModel
    {
        public void Initialize()
        {
            this.Message = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?";
        }

        public string Message { get; set; }
    }
}