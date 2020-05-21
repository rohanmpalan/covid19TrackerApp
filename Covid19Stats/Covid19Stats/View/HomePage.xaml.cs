using Covid19Stats.Model;
using Covid19Stats.ViewModel;
using LightInject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Covid19Stats.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private HomeViewModel ViewModel { get; set; }
        public HomePage()
        {
            InitializeComponent();
            ViewModel = Bootstrapper.Container.GetInstance<HomeViewModel>();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.Initialize();
        }

        private async void CountryList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           var selectedCountry =   e.SelectedItem as Country;
            if(selectedCountry != null)
            {
               await ViewModel.NavigateToDetails(selectedCountry);
            }
        }
    }
}