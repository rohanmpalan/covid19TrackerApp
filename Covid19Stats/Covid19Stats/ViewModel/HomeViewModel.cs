using Covid19Stats.Helper;
using Covid19Stats.Interface;
using Covid19Stats.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Stats.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {

        private string _totalRecovered;
        public string TotalRecovered
        {
            get { return _totalRecovered; }
            set
            {
                _totalRecovered = value;
                RaisePropertyChanged();
            }
        }

        private string _totalConfirmed;
        public string TotalConfirmed
        {
            get { return _totalConfirmed; }
            set
            {
                _totalConfirmed = value;
                RaisePropertyChanged();
            }
        }

        private string _totalDeceased;
        public string TotalDeceased
        {
            get { return _totalDeceased; }
            set
            {
                _totalDeceased = value;
                RaisePropertyChanged();
            }
        }

        private string _newRecovered;
        public string NewRecovered
        {
            get { return _newRecovered; }
            set
            {
                _newRecovered = value;
                RaisePropertyChanged();
            }
        }

        private string _newConfirmed;
        public string NewConfirmed
        {
            get { return _newConfirmed; }
            set
            {
                _newConfirmed = value;
                RaisePropertyChanged();
            }
        }

        private string _newDeceased;
        public string NewDeceased
        {
            get { return _newDeceased; }
            set
            {
                _newDeceased = value;
                RaisePropertyChanged();
            }
        }

        private string _updatedAt;
        public string UpdatedAt
        {
            get { return _updatedAt; }
            set
            {
                _updatedAt = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Country> _countryList;
        public ObservableCollection<Country> CountryList
        {
            get { return _countryList; }
            set
            {
                _countryList = value;
                RaisePropertyChanged();
            }
        }

        private readonly IWorldDataService _worldDataService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        public HomeViewModel(IWorldDataService worldDataService,
            IDialogService dialogService,
            INavigationService navigationService)
        {
            _worldDataService = worldDataService;
            _dialogService = dialogService;
            _navigationService = navigationService;

            CountryList = new ObservableCollection<Country>();
        }

        public async Task Initialize()
        {
            await GetWorldStats();
        }

        public async Task NavigateToDetails(Country selectedCountry)
        {
            await _navigationService.NavigateTo(PageKeys.CountryDetails, selectedCountry);
        }

        private async Task GetWorldStats()
        {
            try
            {
                _dialogService.ShowLoader("");
                SetDefaultValues();
                var result = await _worldDataService.GetTotalStats();
                _dialogService.HideLoader();
                if (result != null)
                {
                    SetStatValues(result);
                }
                else
                {
                    await _dialogService.ShowError("Data is not available!", "Error", "Ok");
                }

            }
            catch (Exception ex)
            {
                _dialogService.HideLoader();
                await _dialogService.ShowError(ex, "Error", "Ok");
            }
        }

        private void SetStatValues(CoronaStats coronaStats)
        {
            TotalConfirmed = NumberFormatter.ConvertToReadableformat(coronaStats.Global.TotalConfirmed);
            TotalRecovered = NumberFormatter.ConvertToReadableformat(coronaStats.Global.TotalRecovered);
            TotalDeceased = NumberFormatter.ConvertToReadableformat(coronaStats.Global.TotalDeaths);
            NewConfirmed = $"[+{NumberFormatter.ConvertToReadableformat(coronaStats.Global.NewConfirmed)}]";
            NewRecovered = $"[+{NumberFormatter.ConvertToReadableformat(coronaStats.Global.NewDeaths)}]";
            NewDeceased = $"[+{NumberFormatter.ConvertToReadableformat(coronaStats.Global.NewRecovered)}]";
            UpdatedAt = coronaStats.UpdatedDate.ToString("MM/dd/yy HH:mm");
            AddCountries(coronaStats);
        }

        private void SetDefaultValues()
        {
            TotalConfirmed = "--";
            TotalRecovered = "--";
            TotalDeceased = "--";
            UpdatedAt = "--";
        }

        private void AddCountries(CoronaStats result)
        {
            CountryList.Clear();
            if (result.Countries != null)
            {
                var orderedCountry = result.Countries.OrderBy(x => x.CountryName);
                foreach (var country in orderedCountry)
                {
                    CountryList.Add(country);
                }
            }
        }
    }
}
