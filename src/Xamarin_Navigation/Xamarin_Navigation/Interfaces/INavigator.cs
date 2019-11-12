using System;
using System.Threading.Tasks;

namespace Xamarin_Navigation.Interfaces
{
    public interface INavigator
    {
        Task PushAsync(Type pageType, object model);

        Task PushModalAsync(Type pageType, object model);

        Task InsertPageBefore(Type pageType, Type beforePageType, object model);
    }
}
