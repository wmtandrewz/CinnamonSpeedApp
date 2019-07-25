using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using SkiaSharp;
using SpeedTest.Models;
using SpeedTest.Services;
using SpeedTest.Views;
using SpeedTest.Helpers;
using Xamarin.Forms;

namespace SpeedTest.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand PageOnLoadCommand { get; }
        public ICommand HotelPickerSelectedChangedCommand { get; }
        public ICommand RoomTextCompletedCommand { get; }
        public ICommand TestButtonCommand { get; }

        private ObservableCollection<APModel> _scanResults;
        public ObservableCollection<APModel> ScanResults
        {
            get
            {
                return _scanResults;
            }
            set
            {
                _scanResults = value;
                OnPropertyChanged("ScanResults");
            }
        }

        private Microcharts.Chart _donutChart;
        public Microcharts.Chart DonutChart
        {
            get
            {
                return _donutChart;
            }

            set
            {
                _donutChart = value;
                OnPropertyChanged("DonutChart");
            }
        }

        private string _targetText = "Target 20";
        public string TargetText
        {
            get
            {
                return _targetText;
            }

            set
            {
                _targetText = value;
                OnPropertyChanged("TargetText");
            }
        }


        public string NameText { get; } = $"Hi! {Settings.FullName}";

        private INavigation Navigation { get; set; }


        public HomeViewModel(INavigation navigation)
        {
            this.Navigation = navigation;


            PageOnLoadCommand = new Command(async () => await PageOnLoad());
            TestButtonCommand = new Command(async () => await TestButtonPressed());

            //Donut Chart
            List<Microcharts.Entry> initialEntries = new List<Microcharts.Entry>
            {
                new Microcharts.Entry(100)
                {
                    Color = SKColor.Parse("#FFFFFF")
                },

                new Microcharts.Entry(0)
                {
                    Color = SKColor.Parse("#008000")
                }
            };

            _donutChart = new Microcharts.RadialGaugeChart { Entries = initialEntries };
           
        }

        private async Task TestButtonPressed()
        {
            await Navigation.PushAsync(new MainPage());
        }


        private async Task PageOnLoad()
        {
            try
            {

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

                var results = await ApiGETservices.GetTestResults(startDate.ToString("yyyy-MM-dd"), endDate);
                if (results != null)
                {
                    var resultList = JsonConvert.DeserializeObject<List<ResultModel>>(results);
                    DisplayCharts(resultList);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void DisplayCharts(List<ResultModel> results)
        {

            int completedCount = results.Count(r => r.Username.ToLower() == Helpers.Settings.UserName.ToLower() && r.Date == DateTime.Now.Date);

            var hotelCounts = results.GroupBy(g => g.HodelCode).OrderBy(r => r.Key).Select(r => Tuple.Create(r.Key, r.Count()));

            var avgDownloadSpeed = results.GroupBy(g => g.HodelCode).OrderBy(r => r.Key).Select(s => Tuple.Create(s.Key, s.Average(r => r.Download)));

            var avgUploadSpeed = results.GroupBy(g => g.HodelCode).OrderBy(r => r.Key).Select(s => Tuple.Create(s.Key, s.Average(r => r.Upload)));

            //Gauge Chart 
            List<Microcharts.Entry> checkinEntries = new List<Microcharts.Entry>
            {
                new Microcharts.Entry(20)
                {
                    Color = SKColor.Parse("#FFFFFF")
                },

                new Microcharts.Entry(completedCount)
                {
                    Color = SKColor.Parse("#008000")
                }
            };

            DonutChart = new Microcharts.RadialGaugeChart { Entries = checkinEntries };

            TargetText = $"{completedCount} of 20";

        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
