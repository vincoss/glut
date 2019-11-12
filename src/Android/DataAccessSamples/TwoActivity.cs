using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Framework;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DataAccessSamples.Adapters;
using DataAccessSamples.Data;
using System.Threading;


namespace DataAccessSamples
{
    [Activity(Label = "TwoActivity")]
    public class TwoActivity : Activity
    {
        private Stock _task = new Stock();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            View titleView = Window.FindViewById(Android.Resource.Id.Title);
            if (titleView != null)
            {
                IViewParent parent = titleView.Parent;
                if (parent != null && (parent is View))
                {
                    View parentView = (View) parent;
                    parentView.SetBackgroundColor(Android.Graphics.Color.Rgb(0x26, 0x75, 0xFF)); //38, 117 ,255
                }
            }

            SetContentView(Resource.Layout.TwoView);

            int stockId = Intent.GetIntExtra("StockId", 0);
            if (stockId > 0)
            {
                _task = StockRepository.GetStock(stockId);
            }

            NameEditText.Text = _task.Name;
            NotesTextEdit.Text = _task.Symbol;
            CancelButton.Text = (_task.Id == 0 ? "Cancel" : "Delete");

            StocksListView.ItemSelected += Spinner_ItemSelected;
            StocksListView.Adapter = new StockAdapter(this, Resource.Layout.TwoListItemView, Model.Stocks); ;

            // button clicks 
            CancelButton.Click += CancelButton_Click;
            SaveButton.Click += SaveButton_Click;
        }

        protected override void OnResume()
        {
            base.OnResume();

            this.Model.Initialize();
        }

        #region View controls event handlers

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            CancelDelete();
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var listView = (ListView) sender;

            var selectedItem = listView.GetItemAtPosition(e.Position);

            string toast = string.Format("The stock is {0}", selectedItem);

            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        #endregion

        #region View controls implementation

        private Button _cancelDeleteButton = null;

        public Button CancelButton
        {
            get { return _cancelDeleteButton ?? (_cancelDeleteButton = FindViewById<Button>(Resource.Id.btnCancelDelete)); }
        }

        private EditText _notesTextEdit = null;

        public EditText NotesTextEdit
        {
            get { return _notesTextEdit ?? (_notesTextEdit = FindViewById<EditText>(Resource.Id.txtNotes)); }
        }

        private EditText _nameTextEdit = null;

        public EditText NameEditText
        {
            get { return _nameTextEdit ?? (_nameTextEdit = FindViewById<EditText>(Resource.Id.txtName)); }
        }

        private Button _saveButton = null;

        public Button SaveButton
        {
            get { return _saveButton ?? (_saveButton = FindViewById<Button>(Resource.Id.btnSave)); }
        }

        private ListView _stocksListView;

        public ListView StocksListView
        {
            get { return _stocksListView ?? (_stocksListView = FindViewById<ListView>(Resource.Id.lstTasks)); }
        }

        #endregion

        private TwoViewModel _model;

        public  TwoViewModel Model
        {
            get
            {
                return _model ?? (_model = new TwoViewModel
                    {
                        RunOnUiThread = this.RunOnUiThread
                    });
            }
        }

        protected void Save()
        {
            this.Model.Add(NameEditText.Text, NotesTextEdit.Text, () => { });
        }

        protected void CancelDelete()
        {
            if (_task.Id != 0)
            {
                StockRepository.DeleteStock(_task);
            }
            Finish();
        }
    }

    public class TwoViewModel : ViewModelBase
    {
        public TwoViewModel()
        {
            this.Stocks = new ObservableCollection<Stock>();
        }

        public override void Initialize()
        {
            if (this.IsInitialized)
            {
                return;
            }
            Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(1000);
                    OnLoadCompleted();
                });
        }

        private void OnLoadCompleted()
        {
            this.RunOnUiThread(() =>
                {

                    foreach (var stock in StockRepository.GetStocks())
                    {
                        this.Stocks.Add(stock);
                    }
                    this.IsInitialized = true;
                });
        }

        public void Add(string symbol, string name, Action completed)
        {
            Task.Factory.StartNew(() =>
                {
                    var stock = new Stock
                        {
                            Symbol = symbol,
                            Name = name
                        };

                    StockRepository.SaveStock(stock);

                    this.Stocks.Add(stock);

                    this.RunOnUiThread(completed);
                });
        }

        public ObservableCollection<Stock> Stocks { get; private set; }
    }
}