﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;



namespace PickerSamples.Controls
{
    /// <summary>
    /// Class BindablePicker.
    /// </summary>
    public class BindablePicker : Picker
    {
        /// <summary>
        /// The HasBorder property
        /// </summary>
        public static readonly BindableProperty HasBorderProperty = BindableProperty.Create("HasBorder", typeof(bool), typeof(ExtendedEntry), true);

        /// <summary>
        /// The title property
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create((BindablePicker w) => w.Title, null);

        /// <summary>
        /// The selected index property
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create<BindablePicker, int>(w => w.SelectedIndex, -1, BindingMode.TwoWay, null, delegate(BindableObject bindable, int oldvalue, int newvalue)
        {
            var selectedIndexChanged = ((BindablePicker)bindable).SelectedIndexChanged;
            if (selectedIndexChanged != null)
            {
                selectedIndexChanged(bindable, EventArgs.Empty);
            }
        }, null, CoerceSelectedIndex);

        /// <summary>
        /// Occurs when [selected index changed].
        /// </summary>
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindablePicker"/> class.
        /// </summary>
        public BindablePicker()
        {
            this.Items = new ObservableCollection<string>();
            this.Items.CollectionChanged += this.OnItemsCollectionChanged;
            this.SelectedIndexChanged += OnSelectedIndexChanged;
        }

        /// <summary>
        /// Gets or sets the souce item label converter.
        /// </summary>
        /// <value>The souce item label converter.</value>
        public Func<object, string> SourceItemLabelConverter { get; set; }

        /// <summary>
        /// The items source property
        /// </summary>
        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create<BindablePicker, IList>(o => o.ItemsSource, default(IList), propertyChanged: OnItemsSourceChanged);

        /// <summary>
        /// The selected item property
        /// </summary>
        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create<BindablePicker, object>(o => o.SelectedItem, default(object), BindingMode.TwoWay,propertyChanged: OnSelectedItemChanged);

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public ObservableCollection<string> Items
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the index of the selected.
        /// </summary>
        /// <value>The index of the selected.</value>
        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets if the border should be shown or not
        /// </summary>
        /// <value><c>true</c> if this instance has border; otherwise, <c>false</c>.</value>
        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }	

        /// <summary>
        /// Called when [items source changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">The oldvalue.</param>
        /// <param name="newvalue">The newvalue.</param>
        private static void OnItemsSourceChanged(BindableObject bindable, IList oldvalue, IList newvalue)
        {
            var picker = bindable as BindablePicker;

            if (picker == null) return;

            picker.Items.Clear();

            if (newvalue == null) return;

            foreach (var item in newvalue)
            {
                picker.Items.Add(picker.SourceItemLabelConverter != null
                    ? picker.SourceItemLabelConverter(item)
                    : item.ToString());
            }
        }

        /// <summary>
        /// Handles the <see cref="E:SelectedIndexChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            this.SelectedItem = (SelectedIndex < 0 || SelectedIndex > Items.Count - 1) ? null : ItemsSource[SelectedIndex];
        }

        /// <summary>
        /// Called when [selected item changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">The oldvalue.</param>
        /// <param name="newvalue">The newvalue.</param>
        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as BindablePicker;

            if (picker == null) return;

            if (newvalue != null) 
            {
                var title = (picker.SourceItemLabelConverter != null) ? picker.SourceItemLabelConverter(newvalue) : newvalue.ToString();
                picker.SelectedIndex = picker.Items.IndexOf(title);
            } 
            else 
            {
                picker.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Coerces the index of the selected.
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        private static int CoerceSelectedIndex(BindableObject bindable, int value)
        {
            var picker = bindable as BindablePicker;
            return (picker == null || picker.Items == null) ? - 1 : value.Clamp(-1, picker.Items.Count-1);
        }

        /// <summary>
        /// Handles the <see cref="E:ItemsCollectionChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (SelectedItem == null) return;

            var title = SourceItemLabelConverter != null ? SourceItemLabelConverter (SelectedItem) : SelectedItem.ToString();
            SelectedIndex = Items.IndexOf (title);
        }
    }

	/// <summary>
	/// An extended entry control that allows the Font and text X alignment to be set
	/// </summary>
	public class ExtendedEntry : Entry
	{
		/// <summary>
		/// The font property
		/// </summary>
		public static readonly BindableProperty FontProperty =
			BindableProperty.Create("Font", typeof(Font), typeof(ExtendedEntry), new Font());

		/// <summary>
		/// The XAlign property
		/// </summary>
		public static readonly BindableProperty XAlignProperty =
			BindableProperty.Create("XAlign", typeof(TextAlignment), typeof(ExtendedEntry), 
			TextAlignment.Start);

		/// <summary>
		/// The HasBorder property
		/// </summary>
		public static readonly BindableProperty HasBorderProperty =
			BindableProperty.Create("HasBorder", typeof(bool), typeof(ExtendedEntry), true);

		/// <summary>
		/// The PlaceholderTextColor property
		/// </summary>
		public static readonly BindableProperty PlaceholderTextColorProperty =
			BindableProperty.Create("PlaceholderTextColor", typeof(Color), typeof(ExtendedEntry), Color.Default);

        /// <summary>
        /// The MaxLength property
        /// </summary>
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create("MaxLength", typeof(int), typeof(ExtendedEntry), int.MaxValue);

        /// <summary>
        /// Gets or sets the MaxLength
        /// </summary>
        public int MaxLength
        {
            get { return (int)this.GetValue(MaxLengthProperty);}
            set { this.SetValue(MaxLengthProperty, value); }
        }

		/// <summary>
		/// Gets or sets the Font
		/// </summary>
		public Font Font
		{
			get { return (Font)GetValue(FontProperty); }
			set { SetValue(FontProperty, value); }
		}

		/// <summary>
		/// Gets or sets the X alignment of the text
		/// </summary>
		public TextAlignment XAlign
		{
			get { return (TextAlignment)GetValue(XAlignProperty); }
			set { SetValue(XAlignProperty, value); }
		}

		/// <summary>
		/// Gets or sets if the border should be shown or not
		/// </summary>
		public bool HasBorder
		{
			get { return (bool)GetValue(HasBorderProperty); }
			set { SetValue(HasBorderProperty, value); }
		}

		/// <summary>
		/// Sets color for placeholder text
		/// </summary>
		public Color PlaceholderTextColor
		{
			get { return (Color)GetValue(PlaceholderTextColorProperty); }
			set { SetValue(PlaceholderTextColorProperty, value); }
		}

		/// <summary>
		/// The left swipe
		/// </summary>
		public EventHandler LeftSwipe;
		/// <summary>
		/// The right swipe
		/// </summary>
		public EventHandler RightSwipe;

		internal void OnLeftSwipe(object sender, EventArgs e)
		{
			var handler = this.LeftSwipe;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		internal void OnRightSwipe(object sender, EventArgs e)
		{
			var handler = this.RightSwipe;
			if (handler != null)
			{
				handler(this, e);
			}
		}
	}

    /// <summary>
    /// Class NumericExtensions.
    /// </summary>
    public static class NumericExtensions
    {
        /// <summary>
        /// Clamps the specified self.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>System.Double.</returns>
        public static double Clamp(this double self, double min, double max)
        {
            return Math.Min(max, Math.Max(self, min));
        }

        /// <summary>
        /// Clamps the specified self.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>System.Int32.</returns>
        public static int Clamp(this int self, int min, int max)
        {
            return Math.Min(max, Math.Max(self, min));
        }
    }


}
