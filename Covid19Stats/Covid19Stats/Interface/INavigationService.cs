using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Covid19Stats.Interface
{
    public interface INavigationService
    {
        NavigationPage CurrentNavigationPage { get; set; }
        string CurrentPageKey { get; }         
        void GoBack();
        Task NavigateTo(string pageKey);
        Task NavigateTo(string pageKey, object parameter);
        void PopToRoot();
        void Configure(string pageKey, Type pageType);
    }
}
