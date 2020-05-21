using Covid19Stats.Model;
using Covid19Stats.ViewModel;
using LightInject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Covid19Stats.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryDetailsPage : ContentPage
    {
        private Country _selectedCountry;
        private CountryDetailsViewModel ViewModel { get; set; }
        public CountryDetailsPage(Country selectedCountry)
        {
            _selectedCountry = selectedCountry;
            InitializeComponent();
            ViewModel = Bootstrapper.Container.GetInstance<CountryDetailsViewModel>();
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Initialize(_selectedCountry);
        }
    }
}