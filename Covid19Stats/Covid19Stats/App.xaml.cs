using Covid19Stats.View;
using Xamarin.Forms;

namespace Covid19Stats
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Bootstrapper.Init();
            MainPage = new NavigationPage(new HomePage());
            Akavache.BlobCache.ApplicationName = "Covid19Stats";
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
