using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Net.Wifi;
using Newtonsoft.Json;
using SkiaSharp;
using SpeedTest.Globals;
using SpeedTest.Interfaces;
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

        private Microcharts.Chart _barChart;
        public Microcharts.Chart BarChart
        {
            get
            {
                return _barChart;
            }

            set
            {
                _barChart = value;
                OnPropertyChanged("BarChart");
            }
        }

        private Microcharts.Chart _lineChartDL;
        public Microcharts.Chart LineChartDL
        {
            get
            {
                return _lineChartDL;
            }

            set
            {
                _lineChartDL = value;
                OnPropertyChanged("LineChartDL");
            }
        }

        private Microcharts.Chart _lineChartDLSumMon;
        public Microcharts.Chart LineChartDLSumMon
        {
            get
            {
                return _lineChartDLSumMon;
            }

            set
            {
                _lineChartDLSumMon = value;
                OnPropertyChanged("LineChartDLSumMon");
            }
        }

        private Microcharts.Chart _lineChartUL;
        public Microcharts.Chart LineChartUL
        {
            get
            {
                return _lineChartUL;
            }

            set
            {
                _lineChartUL = value;
                OnPropertyChanged("LineChartUL");
            }
        }
        private Microcharts.Chart _lineChartULSumMon;
        public Microcharts.Chart LineChartULSumMon
        {
            get
            {
                return _lineChartULSumMon;
            }

            set
            {
                _lineChartULSumMon = value;
                OnPropertyChanged("LineChartULSumMon");
            }
        }

        private Microcharts.Chart _lineChartULHistory;
        public Microcharts.Chart LineChartULHistory
        {
            get
            {
                return _lineChartULHistory;
            }

            set
            {
                _lineChartULHistory = value;
                OnPropertyChanged("LineChartULHistory");
            }
        }

        private Microcharts.Chart _lineChartDLHistory;
        public Microcharts.Chart LineChartDLHistory
        {
            get
            {
                return _lineChartDLHistory;
            }

            set
            {
                _lineChartDLHistory = value;
                OnPropertyChanged("LineChartDLHistory");
            }
        }

        private HotelModel _selectedHotel;
        public HotelModel SelectedHotel
        {
            get
            {
                return _selectedHotel;
            }

            set
            {
                _selectedHotel = value;
                OnPropertyChanged("SelectedHotel");
            }
        }

        private HotelModel _roomSelectedHotel;
        public HotelModel RoomSelectedHotel
        {
            get
            {
                return _roomSelectedHotel;
            }

            set
            {
                _roomSelectedHotel = value;
                OnPropertyChanged("RoomSelectedHotel");
            }
        }

        private List<HotelModel> _hotelList = Constants.HotelList;
        public List<HotelModel> HotelList
        {
            get
            {
                return _hotelList;
            }

            set
            {
                _hotelList = value;
                OnPropertyChanged("HotelList");
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

        private string _roomText = string.Empty;
        public string RoomText
        {
            get
            {
                return _roomText;
            }

            set
            {
                _roomText = value;
                OnPropertyChanged("RoomText");
            }
        }

        public string NameText { get; } = $"Hi! {Settings.FullName}";

        private INavigation Navigation { get; set; }


        public HomeViewModel(INavigation navigation)
        {
            this.Navigation = navigation;


            PageOnLoadCommand = new Command(async () => await PageOnLoad());
            HotelPickerSelectedChangedCommand = new Command(async () => await HotelPickerItemSelected());
            TestButtonCommand = new Command(async () => await TestButtonPressed());
            RoomTextCompletedCommand = new Command(async () => await RoomTextCompleted());

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
            _barChart = new Microcharts.BarChart { Entries = initialEntries };
            _lineChartDL = new Microcharts.LineChart { Entries = initialEntries };
            _lineChartUL = new Microcharts.LineChart { Entries = initialEntries };
            _lineChartDLSumMon = new Microcharts.LineChart { Entries = new List<Microcharts.Entry>() };
            _lineChartULSumMon = new Microcharts.LineChart { Entries = new List<Microcharts.Entry>() };
            _lineChartDLHistory = new Microcharts.LineChart { Entries = new List<Microcharts.Entry>() };
            _lineChartULHistory = new Microcharts.LineChart { Entries = new List<Microcharts.Entry>() };
        }


        private async Task RoomTextCompleted()
        {
            if (RoomSelectedHotel != null && !string.IsNullOrEmpty(RoomText))
            {
                try
                {
                    DateTime now = DateTime.Now;
                    var startDate = new DateTime(now.Year, now.Month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

                    var results = await ApiGETservices.GetRoomHistory(RoomSelectedHotel.HotelCode, Convert.ToInt32(RoomText));
                    if (results != null)
                    {
                        var resultList = JsonConvert.DeserializeObject<List<ResultModel>>(results);
                        DisplayHistoryCharts(resultList);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private async Task TestButtonPressed()
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async Task HotelPickerItemSelected()
        {
            if (SelectedHotel != null)
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
                        DisplaySummeryCharts(resultList, SelectedHotel);

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void DisplaySummeryCharts(List<ResultModel> results, HotelModel hotel)
        {
            var avgDownloadSpeedMonth = results.Where(r => r.HodelCode == hotel.HotelCode).GroupBy(w => CultureInfo.CurrentCulture.Calendar.GetDayOfMonth(w.Date)).OrderBy(r => r.Key).Select(s => Tuple.Create(s.Key, s.Average(r => r.Download)));
            var avUploadSpeedMonth = results.Where(r => r.HodelCode == hotel.HotelCode).GroupBy(w => CultureInfo.CurrentCulture.Calendar.GetDayOfMonth(w.Date)).OrderBy(r => r.Key).Select(s => Tuple.Create(s.Key, s.Average(r => r.Upload)));

            //Line chart DL Month

            List<Microcharts.Entry> LineChartEntriesDLMon = new List<Microcharts.Entry>();

            int l = 0;
            foreach (var item in avgDownloadSpeedMonth)
            {
                var colorCode = Constants.ColorList[l];

                var entry = new Microcharts.Entry((float)item.Item2)
                {
                    Color = SKColor.Parse(colorCode),
                    Label = item.Item1.ToString(),
                    ValueLabel = item.Item2.ToString("F") + " kbps"
                };

                LineChartEntriesDLMon.Add(entry);
                l++;
            }

            LineChartDLSumMon = new Microcharts.LineChart { Entries = LineChartEntriesDLMon, LabelTextSize = 24, LineMode = Microcharts.LineMode.Straight };

            //Line chart UL Month

            List<Microcharts.Entry> LineChartEntriesULMon = new List<Microcharts.Entry>();

            int m = 0;
            foreach (var item in avUploadSpeedMonth)
            {
                var colorCode = Constants.ColorList[m];

                var entry = new Microcharts.Entry((float)item.Item2)
                {
                    Color = SKColor.Parse(colorCode),
                    Label = item.Item1.ToString(),
                    ValueLabel = item.Item2.ToString("F") + " kbps"
                };

                LineChartEntriesULMon.Add(entry);
                m++;
            }

            LineChartULSumMon = new Microcharts.LineChart { Entries = LineChartEntriesULMon, LabelTextSize = 24, LineMode = Microcharts.LineMode.Straight };
        }

        private void DisplayHistoryCharts(List<ResultModel> results)
        {
            var avgDownloadSpeed = results.GroupBy(w => CultureInfo.CurrentCulture.Calendar.GetDayOfMonth(w.Date)).OrderBy(r => r.Key).Select(s => Tuple.Create(s.Key, s.Average(r => r.Download)));
            var avUploadSpeed = results.GroupBy(w => CultureInfo.CurrentCulture.Calendar.GetDayOfMonth(w.Date)).OrderBy(r => r.Key).Select(s => Tuple.Create(s.Key, s.Average(r => r.Upload)));

            //Line chart DL Month

            List<Microcharts.Entry> LineChartEntriesDLHis = new List<Microcharts.Entry>();

            int l = 0;
            foreach (var item in avgDownloadSpeed)
            {
                var colorCode = Constants.ColorList[l];

                var entry = new Microcharts.Entry((float)item.Item2)
                {
                    Color = SKColor.Parse(colorCode),
                    Label = item.Item1.ToString(),
                    ValueLabel = item.Item2.ToString("F") + " kbps"
                };

                LineChartEntriesDLHis.Add(entry);
                l++;
            }

            LineChartDLHistory = new Microcharts.LineChart { Entries = LineChartEntriesDLHis, LabelTextSize = 24, LineMode = Microcharts.LineMode.Straight };

            //Line chart UL Month

            List<Microcharts.Entry> LineChartEntriesULHis = new List<Microcharts.Entry>();

            int m = 0;
            foreach (var item in avUploadSpeed)
            {
                var colorCode = Constants.ColorList[m];

                var entry = new Microcharts.Entry((float)item.Item2)
                {
                    Color = SKColor.Parse(colorCode),
                    Label = item.Item1.ToString(),
                    ValueLabel = item.Item2.ToString("F") + " kbps"
                };

                LineChartEntriesULHis.Add(entry);
                m++;
            }

            LineChartULHistory = new Microcharts.LineChart { Entries = LineChartEntriesULHis, LabelTextSize = 24, LineMode = Microcharts.LineMode.Straight };
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

            //Bar Chart

            List<Microcharts.Entry> barChartEntries = new List<Microcharts.Entry>();

            int i = 0;
            foreach (var item in hotelCounts)
            {
                var colorCode = Constants.ColorList[i];

                var entry = new Microcharts.Entry(item.Item2)
                {
                    Color = SKColor.Parse(colorCode),
                    Label = item.Item1,
                    ValueLabel = item.Item2.ToString("F")
                };
                barChartEntries.Add(entry);

                i++;
            }

            BarChart = new Microcharts.BarChart { Entries = barChartEntries, LabelTextSize = 24 };

            //Line Chart DL
            List<Microcharts.Entry> LineChartEntriesDL = new List<Microcharts.Entry>();

            int j = 0;
            foreach (var item in avgDownloadSpeed)
            {
                var colorCode = Constants.ColorList[j];

                var entry = new Microcharts.Entry((float)item.Item2)
                {
                    Color = SKColor.Parse(colorCode),
                    Label = item.Item1,
                    ValueLabel = item.Item2.ToString("F") + " kbps"
                };

                LineChartEntriesDL.Add(entry);
                j++;
            }

            LineChartDL = new Microcharts.LineChart { Entries = LineChartEntriesDL, LabelTextSize = 24, LineMode = Microcharts.LineMode.Straight };

            //Line Chart UL
            List<Microcharts.Entry> LineChartEntriesUL = new List<Microcharts.Entry>();

            int k = 0;
            foreach (var item in avgUploadSpeed)
            {
                var colorCode = Constants.ColorList[k];

                var entry = new Microcharts.Entry((float)item.Item2)
                {
                    Color = SKColor.Parse(colorCode),
                    Label = item.Item1,
                    ValueLabel = item.Item2.ToString("F") + " kbps"
                };

                LineChartEntriesUL.Add(entry);
                k++;
            }

            LineChartUL = new Microcharts.LineChart { Entries = LineChartEntriesUL, LabelTextSize = 24, LineMode = Microcharts.LineMode.Straight };


        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
