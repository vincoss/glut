using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Android;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Framework;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Specialized;


namespace SpinnerSamples
{
    [Activity(Label = "OneActivity")]
    public class OneActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.OneView);

            Spinner.ItemSelected += Spinner_ItemSelected;

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            Spinner.Adapter = adapter;
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;

            string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));

            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private Spinner _spinner;

        public Spinner Spinner
        {
            get { return _spinner ?? (_spinner = FindViewById<Spinner>(Resource.Id.spinner)); }
        }
    }

    [Activity(Label = "OneActivity")]
    public class OneMvvmActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.OneView);

            Spinner.ItemSelected += Spinner_ItemSelected;

            var adapter = new PlanetAdapter(this, Resource.Layout.OneListViewItem, this.Model.Planets);
            Spinner.Adapter = adapter;

            this.AddButton.Click += AddButton_Click;
        }

        protected override void OnResume()
        {
            base.OnResume();

            this.Model.Initialize();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var planet = new Planet
                {
                    Name = this.AddPlanetName.Text,
                    Description = this.AddPlanetDescription.Text
                };
            this.Model.Planets.Add(planet);
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;

            var selectedItem = spinner.GetItemAtPosition(e.Position);

            string toast = string.Format("The planet is {0}", selectedItem);

            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private OneViewModel _model;

        public OneViewModel Model
        {
            get
            {
                return _model ?? (_model = new OneViewModel
                    {
                        RunOnUiThread = this.RunOnUiThread
                    });
            }
        }

        private Spinner _spinner;

        public Spinner Spinner
        {
            get { return _spinner ?? (_spinner = FindViewById<Spinner>(Resource.Id.spinner)); }
        }

        private EditText _addPlanetName;

        public EditText AddPlanetName
        {
            get { return _addPlanetName ?? (_addPlanetName = FindViewById<EditText>(Resource.Id.AddPlanetName)); }
        }

        private EditText _addPlanetDescription;

        public EditText AddPlanetDescription
        {
            get { return _addPlanetDescription ?? (_addPlanetDescription = FindViewById<EditText>(Resource.Id.AddPlanetDescription)); }
        }

        private Button _addButton;

        public Button AddButton
        {
            get { return _addButton ?? (_addButton = FindViewById<Button>(Resource.Id.AddButton)); }
        }
    }

    public class PlanetAdapter : ObservableCollectionAdapter<Planet>
    {
        public PlanetAdapter(Activity context, int resource, ObservableCollection<Planet> items) : base(context, resource, items)
        {
        }

        protected override void PrepareView(Planet item, View view)
        {
            view.FindViewById<TextView>(Resource.Id.PlanetName).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.PlanetDescription).Text = item.Description;
        }

        protected override long GetItemId(Planet item, int position)
        {
            return 0;
        }
    }

    public class OneViewModel : ViewModelBase
    {
        public OneViewModel()
        {
            this.Planets = new ObservableCollection<Planet>();
        }

        public override void Initialize()
        {
            if (this.IsInitialized)
            {
                return;
            }
            Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(5000);
                    OnLoadCompleted();
                });
        }

        private void OnLoadCompleted()
        {
            this.RunOnUiThread(() =>
                {
                    var items = new[]
                    {
                        new Planet { Name = "Mercury", Description = "Mercury is the smallest and closest to the Sun of the eight planets in the Solar System, with an orbital period of about 88 Earth days."},
                        new Planet { Name = "Venus", Description = "Venus is the second planet from the Sun, orbiting it every 224.7 Earth days. It has no natural satellite."},
                        new Planet { Name = "Earth", Description = "Earth is a terrestrial planet, meaning that it is a rocky body, rather than a gas giant like Jupiter."},
                        new Planet { Name = "Mars", Description = "Mars is the fourth planet from the Sun and the second smallest planet in the Solar System, after Mercury."},
                        new Planet { Name = "Jupiter", Description = "Jupiter is the fifth planet from the Sun and the largest planet in the Solar System. It is a gas giant with mass one-thousandth of that of the Sun but is two and a half times the mass of all the other planets in the Solar System combined. "},
                        new Planet { Name = "Saturn", Description = "Saturn is the sixth planet from the Sun and the second largest planet in the Solar System, after Jupiter. Named after the Roman god of agriculture, its astronomical symbol represents the god's sickle."},
                        new Planet { Name = "Uranus", Description = "Uranus is the seventh planet from the Sun. It has the third-largest planetary radius and fourth-largest planetary mass in the Solar System."},
                        new Planet { Name = "Neptune", Description = "Neptune is the eighth and farthest planet from the Sun in the Solar System. It is the fourth-largest planet by diameter and the third-largest by mass. Among the gaseous planets in the Solar System, Neptune is the most dense."},
                        new Planet { Name = "Pluto", Description = "Pluto is the largest object in the Kuiper belt, and the tenth-most-massive body observed directly orbiting the Sun. It is the second-most-massive known dwarf planet, after Eris."}
                    };

                    foreach (var planet in items)
                    {
                        this.Planets.Add(planet);
                    }

                    this.IsInitialized = true;
                });
        }

        public ObservableCollection<Planet> Planets { get; private set; }
    }

    public class Planet
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return this.Name ?? base.ToString();
        }
    }
}