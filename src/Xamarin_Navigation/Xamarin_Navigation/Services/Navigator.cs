using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Navigation.Interfaces;
using Xamarin_Navigation.Views;

namespace Xamarin_Navigation.Services
{
    public class Navigator : INavigator
    {
        public Task InsertPageBefore(Type pageType, Type beforePageType, object model)
        {
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }
            if (beforePageType == null)
            {
                throw new ArgumentNullException(nameof(beforePageType));
            }
            var page = Get(pageType);
            if (page != null && model != null)
            {
                page.BindingContext = model;
            }
            var previous = Application.Current.MainPage.Navigation.NavigationStack.SingleOrDefault(x => x.GetType() == beforePageType);
            if(previous == null)
            {
                throw new InvalidOperationException(beforePageType.FullName);
            }
            Application.Current.MainPage.Navigation.InsertPageBefore(page, previous);
            Application.Current.MainPage.Navigation.RemovePage(previous);
            return Task.CompletedTask;
        }

        public Task PushAsync(Type pageType, object model)
        {
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }
            var page = Get(pageType);
            if (page != null && model != null)
            {
                page.BindingContext = model;
            }
            return Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public  Task PushModalAsync(Type pageType, object model)
        {
            if(pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }
            var page = Get(pageType);
            if(page != null && model != null)
            {
                page.BindingContext = model;
            }
            var npage = new NavigationPage(page); // TO show navigation bar
           return  Application.Current.MainPage.Navigation.PushModalAsync(npage);
        }

        private Page Get(Type type)
        {
            if (type == typeof(SettingOneView))
            {
                return new SettingOneView();
            }
            if (type == typeof(SettingTwoView))
            {
                return new SettingTwoView();
            }

            return new NotFoundView();
        }
    }
}
