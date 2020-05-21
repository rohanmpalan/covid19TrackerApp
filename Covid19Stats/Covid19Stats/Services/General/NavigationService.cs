using Covid19Stats.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Covid19Stats.Services.General
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pagesByKey;
        public string CurrentPageKey { get; set; }

        public NavigationService()
        {
            _pagesByKey = new Dictionary<string, Type>();
        }

        public NavigationPage CurrentNavigationPage
        {
            get; set;
        }
        public NavigationPage MainPage
        {
            get { return (NavigationPage)Application.Current.MainPage; }
        }

        public void Configure(string pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        #region INavigationServiceExtended implementation

        public async void GoBack()
        {
            try
            {
                await MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task NavigateTo(string pageKey)
        {
           await NavigateTo(pageKey, null);
            
        }

        public async Task NavigateTo(string pageKey, object parameter)
        {
            try
            {
                object[] parameters = null;
                if (parameter != null)
                {
                    parameters = new[] { parameter };
                }
                if (_pagesByKey != null)
                {
                    var displayPage = (Page)Activator.CreateInstance(_pagesByKey[pageKey], parameters);
                    CurrentPageKey = pageKey;

                    await MainPage.Navigation.PushAsync(displayPage);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Debugger.Break();
            }
        }

        public async void PopToRoot()
        {
            await MainPage.Navigation.PopToRootAsync();
        }

        #endregion
    }
}
