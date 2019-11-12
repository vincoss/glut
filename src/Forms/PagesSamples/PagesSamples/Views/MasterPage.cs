using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace PagesSamples.Views
{
    public class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            Label header = new Label
            {
                Text = "Master Detail Page",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Create the master page with the ListView.
            this.Master = new ContentPage
            {
                Title = header.Text,
                Content = new StackLayout
                {
                    Children = 
                    {
                        header, 
                       new Label { Text = "Hello MasterPage" }
                    }
                }
            };

            var contentPage = new ContentPage
            {
                Content = new StackLayout
                {
                    Children = 
                    {
                        new Label { Text = "Hello ContentPage" }
                    }
                }
            };
            
            this.IsPresented = true; // set to false and detail page will be shown first
            
            // Create the detail page using NamedColorPage and wrap it in a
            // navigation page to provide a NavigationBar and Toggle button
            this.Detail = contentPage;

            // For Windows Phone, provide a way to get back to the master page.
            if (Device.OS == TargetPlatform.WinPhone)
            {
                (this.Detail as ContentPage).Content.GestureRecognizers.Add(
                    new TapGestureRecognizer((view) =>
                    {
                        this.IsPresented = true;
                    }));
            }

        }
    }
}
