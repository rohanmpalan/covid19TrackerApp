using Covid19Stats.Interface;
using Covid19Stats.Model;
using Covid19Stats.Helper;
using Microcharts;
using System.Collections.Generic;
using SkiaSharp;

namespace Covid19Stats.ViewModel
{
    class CountryDetailsViewModel : BaseViewModel
    {
        private Country _selectedCountry;
        public Country SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                RaisePropertyChanged();
            }
        }

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

        private Chart _confirmedCasesChart;
        public Chart ConfirmedCasesChart
        {
            get { return _confirmedCasesChart; }
            set
            {
                _confirmedCasesChart = value;
                RaisePropertyChanged();
            }
        }
        public CountryDetailsViewModel()
        {

        }

        public void Initialize(Country selectedCountry)
        {
            SetValues(selectedCountry);
            CreateChart();
        }

        private void SetValues(Country selectedCountry)
        {
            SelectedCountry = selectedCountry;
            TotalConfirmed = NumberFormatter.ConvertToReadableformat(SelectedCountry.TotalConfirmed);
            TotalRecovered = NumberFormatter.ConvertToReadableformat(SelectedCountry.TotalRecovered);
            TotalDeceased = NumberFormatter.ConvertToReadableformat(SelectedCountry.TotalDeaths);
            NewConfirmed = $"[+{NumberFormatter.ConvertToReadableformat(SelectedCountry.NewConfirmed)}]";
            NewRecovered = $"[+{NumberFormatter.ConvertToReadableformat(SelectedCountry.NewDeaths)}]";
            NewDeceased = $"[+{NumberFormatter.ConvertToReadableformat(SelectedCountry.NewRecovered)}]";
            UpdatedAt = SelectedCountry.UpdatedDate.ToString("MM/dd/yy HH:mm");
        }

        private void CreateChart()
        {
            var TotalActive = SelectedCountry.TotalConfirmed - (SelectedCountry.TotalDeaths + SelectedCountry.TotalRecovered);
            List<Entry> ChartEntries = new List<Entry>();
            ChartEntries.Add(new Entry(TotalActive)
            {
                Label = "Active",
                ValueLabel = NumberFormatter.ConvertToReadableformat(TotalActive),
                Color = SKColor.Parse("#ff4a3d")
            });
            ChartEntries.Add(new Entry(SelectedCountry.TotalRecovered)
            {
                Label = "Recovered",
                ValueLabel = TotalRecovered,
                Color = SKColor.Parse("#77d065")
            });
            ChartEntries.Add(new Entry(SelectedCountry.TotalDeaths)
            {
                Label = "Deceased",
                ValueLabel = TotalDeceased,
                Color = SKColor.Parse("#82807a")
            });


            ConfirmedCasesChart = new BarChart()
            {
                Entries = ChartEntries,
                BackgroundColor = SKColors.Transparent,
                PointMode = PointMode.None,
                LabelTextSize = 24,
            };
        }
    }
}

