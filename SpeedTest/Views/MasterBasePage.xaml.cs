using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeedTest.Globals;
using SpeedTest.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeedTest.Views
{
    public partial class MasterBasePage : MasterDetailPage
    {
        public MasterBasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            MasterPage.BindingContext = new MasterViewModel(this);

            var navPage = new NavigationPage(new HomeView())
            {
                BarTextColor = Color.White,
                BackgroundColor = Color.White,
                BarBackgroundColor = Color.FromHex("#211261")
            };

            Constants.MasterDetailNavigation = navPage.Navigation;

            Detail = navPage;
        }

    }
}
